using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ArchiveComparer2.Library
{
    public class ArchiveDuplicateDetector : IDisposable
    {
        public delegate void NotifyEventHandler(object sender, NotifyEventArgs e);

        public event NotifyEventHandler Notify;

        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        private ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        private Thread _thread;

        #region ThreadHelper

        public void Pause()
        {
            _pauseEvent.Reset();
            NotifyCaller("Paused", OperationStatus.PAUSED);
        }

        public void Resume()
        {
            _pauseEvent.Set();
            NotifyCaller("Resumed", OperationStatus.RESUMED);
        }

        public void Stop()
        {
            // Signal the shutdown event
            _shutdownEvent.Set();

            // Make sure to resume any paused threads
            _pauseEvent.Set();

            NotifyCaller("Stopped", OperationStatus.STOPPED);

            if (_thread != null)
            {
                // Wait for the thread to exit
                _thread.Join();
            }
        }

        private void NotifyCaller(string message, OperationStatus status, List<DuplicateArchiveInfoList> dupList = null, int curr = 0, int total = 0)
        {
            Notify?.Invoke(this, new NotifyEventArgs() { Message = message, Status = status, DupList = dupList, TotalCount = total, CurrentCount = curr });
        }

        private void SearchThreadingImpl(object option)
        {
            Search((DuplicateSearchOption)option);
        }

        #endregion ThreadHelper

        #region Main Logic

        /// <summary>
        /// Step 1 - build file list from given paths
        /// </summary>
        /// <param name="option"></param>
        /// <returns>List of FileInfo</returns>
        private List<FileInfo> BuildFileList(DuplicateSearchOption option)
        {
            NotifyCaller("Start building file list.", OperationStatus.BUILDING_FILE_LIST);

            List<FileInfo> fileList = new List<FileInfo>();
            Regex re = new Regex(option.FilePattern, option.FileCaseInsensitive ? RegexOptions.IgnoreCase : RegexOptions.None);

            int i = 1;
            int t = option.Paths.Count;
            foreach (var path in option.Paths)
            {
                try
                {
                    NotifyCaller("Building file list: " + path, OperationStatus.BUILDING_FILE_LIST, curr: i, total: t);
                    DirectoryInfo dirList = new DirectoryInfo(path);
                    FileInfo[] tempList = dirList.GetFiles("*", SearchOption.AllDirectories);

                    // filter based on filepattern
                    var filteredList = from f in tempList
                                       where re.IsMatch(f.Name)
                                       select f;

                    fileList.AddRange(filteredList);
                    ++i;
                }
                catch (Exception ex)
                {
                    NotifyCaller(ex.Message + " (" + path + ")", OperationStatus.ERROR);
                }
            }

            NotifyCaller("Total File: " + fileList.Count, OperationStatus.BUILDING_FILE_LIST, total: fileList.Count);

            return fileList;
        }

        /// <summary>
        /// Step 2: calculate crc
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="option"></param>
        /// <returns>List DuplicateArchiveInfo</returns>
        private List<DuplicateArchiveInfo> CalculateCRC(List<FileInfo> fileList, DuplicateSearchOption option)
        {
            List<DuplicateArchiveInfo> list = new List<DuplicateArchiveInfo>();

            int i = 0;
            foreach (FileInfo f in fileList)
            {
                _pauseEvent.WaitOne(Timeout.Infinite);
                if (_shutdownEvent.WaitOne(0))
                    break;

                //NotifyCaller(f.FullName, OperationStatus.CALCULATING_CRC, curr: i, total: fileList.Count);
                //string msg = String.Format("File {0} of {1}", i, fileList.Count);
                NotifyCaller("", OperationStatus.CALCULATING_CRC, curr: i, total: fileList.Count);
                try
                {
                    DuplicateArchiveInfo item = Util.GetArchiveInfo(f.FullName, option);
                    item.FileSize = f.Length;
                    item.CreationTime = f.CreationTime;

                    list.Add(item);
                }
                catch (Exception ex)
                {
                    string message = ex.Message + " (" + f.FullName + ")";
                    NotifyCaller(message, OperationStatus.ERROR);
                }
                ++i;
            }

            NotifyCaller("Complete calculating CRC, total: " + list.Count, OperationStatus.CALCULATING_CRC, total: list.Count);

            return list;
        }

        /// <summary>
        /// Step 3: Build duplicate list
        /// </summary>
        /// <param name="list"></param>
        /// <param name="limit"></param>
        /// <param name="ignoreLimit"></param>
        private List<DuplicateArchiveInfoList> BuildDuplicateList(List<DuplicateArchiveInfo> list, DuplicateSearchOption option)
        {
            NotifyCaller("Start building duplicate list.", OperationStatus.BUILDING_DUPLICATE_LIST);

            List<DuplicateArchiveInfoList> dupList = new List<DuplicateArchiveInfoList>();

            list.Sort(new DuplicateArchiveInfoItemCountComparer());

            int totalCount = list.Count;
            int i = 0;
            while (list.Count > 0)
            {
                _pauseEvent.WaitOne(Timeout.Infinite);
                if (_shutdownEvent.WaitOne(0))
                {
                    NotifyCaller("Stopping...", OperationStatus.BUILDING_DUPLICATE_LIST);
                    break;
                }

                ++i;
                DuplicateArchiveInfoList dup = new DuplicateArchiveInfoList();
                DuplicateArchiveInfo original = list[0];
                list.RemoveAt(0);
                dup.Original = original;

                string message = "Checking: " + original.Filename + " ( Duplicate group found: " + i + " Remaining: " + list.Count + ")";
                //NotifyCaller(message, OperationStatus.BUILDING_DUPLICATE_LIST, curr: i, total: totalCount);
                NotifyCaller("", OperationStatus.BUILDING_DUPLICATE_LIST, curr: i, total: totalCount);

                // parallel method
                if (option.TaskLimit > 1)
                {
                    var taskScheduler = new Nandaka.Common.LimitedConcurrencyLevelTaskScheduler(option.TaskLimit, 16);
                    var pOption = new ParallelOptions()
                    {
                        TaskScheduler = taskScheduler
                    };

                    Parallel.For(0, list.Count, pOption, (innerIdx) =>
                  {
                      DuplicateArchiveInfo curr = list[innerIdx];
                      if (curr.IsRemoved) return;

                      if (Compare(ref original, ref curr, option))
                      {
                          if (dup.Duplicates == null) dup.Duplicates = new List<DuplicateArchiveInfo>();
                          // remove from the source list.
                          lock (list)
                          {
                              curr.IsRemoved = true;
                          }
                          dup.Duplicates.Add(curr);
                      }
                  });
                }
                else
                {
                    // check for other possible dups.
                    int index = 0;
                    while (list.Count > index)
                    {
                        DuplicateArchiveInfo curr = list[index];
                        if (Compare(ref original, ref curr, option))
                        {
                            if (dup.Duplicates == null) dup.Duplicates = new List<DuplicateArchiveInfo>();
                            dup.Duplicates.Add(curr);
                            // remove from the source list.
                            list.Remove(curr);
                            --totalCount;
                        }
                        else
                        {
                            ++index;
                        }
                    }
                }
                if (dup.Duplicates != null && dup.Duplicates.Count > 0)
                    dupList.Add(dup);
            }

            foreach (DuplicateArchiveInfoList dup in dupList)
            {
                if (dup.Duplicates != null)
                {
                    dup.Duplicates.Sort(new DuplicateArchiveInfoPercentageComparer());
                }
            }
            NotifyCaller("Building Duplicate List Complete.", OperationStatus.BUILDING_DUPLICATE_LIST);

            return dupList;
        }

        /// <summary>
        /// Check if file is duplicated
        /// </summary>
        /// <param name="original"></param>
        /// <param name="duplicate"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        private bool Compare(ref DuplicateArchiveInfo original, ref DuplicateArchiveInfo duplicate, DuplicateSearchOption option)
        {
            lock (original)
            {
                //NotifyCaller("Comparing: " + Origin.Filename + " to " + Duplicate.Filename, OperationStatus.COMPARING);

                // if the match type already changed from original, skip it
                // most likely already validated by other task
                if (original.MatchType != MatchType.ORIGINAL)
                    return false;

                // if item count is equal, try to check from crc strings.

                original.MatchType = MatchType.ORIGINAL;
                original.Percentage = 0.0;
                if (original.NoMatches != null) original.NoMatches.Clear();

                if (original.Items.Count == duplicate.Items.Count)
                {
                    if (original.ToCRCString() == duplicate.ToCRCString())
                    {
                        //NotifyCaller("CRC Strings are equal.", OperationStatus.COMPARING);
                        duplicate.Percentage = 100.0;
                        duplicate.MatchType = MatchType.EQUALCOUNT;
                        return true;
                    }
                    else if (option.OnlyPerfectMatch)
                    {
                        return false;
                    }
                }

                // Check each files in duplicate
                int limitCount;

                // if only have 'IgnoreLimit' files, then all must match
                if (option.IgnoreLimit > duplicate.Items.Count) limitCount = 0;
                else limitCount = duplicate.Items.Count - (duplicate.Items.Count * option.Limit / 100);

                int skippedCount = 0;
                int i = 0;
                int j = 0;
                while (i < original.Items.Count && j < duplicate.Items.Count && skippedCount <= limitCount)
                {
                    // compare the from the biggest crc.
                    int result = string.Compare(original.Items[i].Crc, duplicate.Items[j].Crc, true, System.Globalization.CultureInfo.InvariantCulture);
                    if (result == 0)
                    {
                        ++i; ++j;
                    }
                    else if (result > 0)
                    {
                        // Origin file skipped
                        ++i;
                    }
                    else
                    {
                        // Duplicate file skipped, no match in Origin
                        ++skippedCount;
                        if (duplicate.NoMatches == null) duplicate.NoMatches = new List<ArchiveFileInfoSmall>();
                        duplicate.NoMatches.Add(duplicate.Items[j]);
                        ++j;
                    }
                }

                if (j < duplicate.Items.Count)
                {
                    if (duplicate.NoMatches == null) duplicate.NoMatches = new List<ArchiveFileInfoSmall>();
                    duplicate.NoMatches.AddRange(duplicate.Items.GetRange(j, duplicate.Items.Count - j));
                    skippedCount = duplicate.NoMatches.Count;
                }

                double percent = (double)(duplicate.Items.Count - skippedCount) / duplicate.Items.Count * 100;
                if (percent >= option.Limit && skippedCount < limitCount)
                {
                    //NotifyCaller("Match: " + percent + "%", OperationStatus.COMPARING);
                    duplicate.Percentage = percent;
                    duplicate.MatchType = MatchType.SUBSET;
                    return true;
                }

                //NotifyCaller("Not Match", OperationStatus.COMPARING);
                if (duplicate.NoMatches != null) duplicate.NoMatches.Clear();
                return false;
            }
        }

        /// <summary>
        /// Step 4: Clean up no duplicate
        /// </summary>
        /// <param name="dupList"></param>
        /// <returns></returns>
        private List<DuplicateArchiveInfoList> CleanUpDuplicate(List<DuplicateArchiveInfoList> dupList)
        {
            int index = 0;
            while (index < dupList.Count)
            {
                NotifyCaller(" Cleaning " + (index + 1) + " of " + dupList.Count, OperationStatus.FILTERING);
                if (dupList[index].Duplicates == null)
                {
                    NotifyCaller("Removing: " + dupList[index].Original.Filename, OperationStatus.FILTERING);
                    dupList.RemoveAt(index);
                }
                else
                {
                    dupList[index].Original.DupGroup = index;
                    if (dupList[index] != null)
                    {
                        foreach (var dup in dupList[index].Duplicates)
                        {
                            dup.DupGroup = index;
                        }
                        ++index;
                    }
                    else
                    {
                        NotifyCaller("Removing: " + dupList[index].Original.Filename, OperationStatus.FILTERING);
                        dupList.RemoveAt(index);
                    }
                }
            }
            return dupList;
        }

        #endregion Main Logic

        public List<DuplicateArchiveInfoList> Search(DuplicateSearchOption option)
        {
            var start = DateTime.Now.Ticks;
            NotifyCaller("Target Count: " + option.Paths.Count, OperationStatus.READY);
            NotifyCaller("Thread Limit: " + option.TaskLimit, OperationStatus.READY);

            if (option.PreventStanby)
            {
                NotifyCaller("Disabling Sleep", OperationStatus.READY);
                Util.PreventSleep();
            }

            List<FileInfo> fileList = BuildFileList(option);
            List<DuplicateArchiveInfo> list = CalculateCRC(fileList, option);
            List<DuplicateArchiveInfoList> dupList = BuildDuplicateList(list, option);
            dupList = CleanUpDuplicate(dupList);

            NotifyCaller("Completed in: " + new TimeSpan(DateTime.Now.Ticks - start), OperationStatus.COMPLETE);
            NotifyCaller("Total: " + dupList.Count + " duplicate groups", OperationStatus.COMPLETE, dupList, total: dupList.Count);
            Util.AllowStanby();

            return dupList;
        }

        public void SearchThreading(DuplicateSearchOption option)
        {
            ParameterizedThreadStart ts = new ParameterizedThreadStart(SearchThreadingImpl);
            if (_thread == null || _thread.ThreadState == ThreadState.Stopped)
            {
                _thread = new Thread(ts);
                _thread.Priority = ThreadPriority.Lowest;
                _thread.Start(option);
            }
        }

        public void ChangeThreadPriority(ThreadPriority priority)
        {
            if (_thread != null)
            {
                _thread.Priority = priority;
            }
        }

        private void Dispose(bool disposing)
        {
            if (_thread != null && _thread.IsAlive)
            {
                this.Stop();
            }
            if (_pauseEvent != null) _pauseEvent.Close();
            if (_shutdownEvent != null) _shutdownEvent.Close();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}