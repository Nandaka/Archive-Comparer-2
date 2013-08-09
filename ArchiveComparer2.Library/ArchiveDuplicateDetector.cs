using System;
using System.Collections.Generic;
using System.Linq;

using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace ArchiveComparer2.Library
{

    public class ArchiveDuplicateDetector:IDisposable
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
            if (Notify != null )
            {
                Notify(this, new NotifyEventArgs() { Message = message, Status = status, DupList = dupList, TotalCount = total, CurrentCount = curr });
            }
        }

        private void SearchThreadingImpl(object option)
        {

            Search((DuplicateSearchOption)option);
        }

        #endregion

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

                NotifyCaller(f.FullName, OperationStatus.CALCULATING_CRC, curr:i, total:fileList.Count);
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

            NotifyCaller("Complete calculating CRC, total: " + list.Count, OperationStatus.CALCULATING_CRC, total:list.Count);

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
                DuplicateArchiveInfo temp = list[0];
                list.RemoveAt(0);
                dup.Original = temp;

                string message = "Checking: " + temp.Filename + " ( Duplicate group found: " + i + " File to check left: " + list.Count + ")";
                NotifyCaller(message, OperationStatus.BUILDING_DUPLICATE_LIST, curr:i, total:totalCount);

                // check for other possible dups.
                int index = 0;
                while (list.Count > index)
                {
                    DuplicateArchiveInfo curr = list[index];

                    if (Compare(ref temp, ref curr, option))
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
        /// <param name="Origin"></param>
        /// <param name="Duplicate"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        private bool Compare(ref DuplicateArchiveInfo Origin, ref DuplicateArchiveInfo Duplicate, DuplicateSearchOption option)
        {
            NotifyCaller("Comparing: " + Origin.Filename + " to " + Duplicate.Filename, OperationStatus.COMPARING);
            
            // if item count is equal, try to check from crc strings.
            Origin.MatchType = MatchType.ORIGINAL;
            Origin.Percentage = 0.0;
            if (Origin.NoMatches != null) Origin.NoMatches.Clear();

            if (Origin.Items.Count == Duplicate.Items.Count)
            {
                Duplicate.MatchType = MatchType.EQUALCOUNT;

                if (Origin.ToCRCString() == Duplicate.ToCRCString())
                {
                    NotifyCaller("CRC Strings are equal.", OperationStatus.COMPARING);
                    Duplicate.Percentage = 100.0;
                    return true;
                }
                else if (option.OnlyPerfectMatch)
                {
                    return false;
                }
            }

            Duplicate.MatchType = MatchType.SUBSET;

            // Check each files in duplicate
            int limitCount;

            // if only have 'IgnoreLimit' files, then all must match
            if (option.IgnoreLimit > Duplicate.Items.Count) limitCount = 0;
            else limitCount = Duplicate.Items.Count - (Duplicate.Items.Count * option.Limit / 100);

            int skippedCount = 0;
            int i = 0;
            int j = 0;
            while (i < Origin.Items.Count && j < Duplicate.Items.Count && skippedCount <= limitCount)
            {
                // compare the from the biggest crc.
                int result = string.Compare(Origin.Items[i].Crc, Duplicate.Items[j].Crc, true, System.Globalization.CultureInfo.InvariantCulture);
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
                    if (Duplicate.NoMatches == null) Duplicate.NoMatches = new List<ArchiveFileInfoSmall>();
                    Duplicate.NoMatches.Add(Duplicate.Items[j]);
                    ++j;
                }
            }

            if (j < Duplicate.Items.Count)
            {
                if (Duplicate.NoMatches == null) Duplicate.NoMatches = new List<ArchiveFileInfoSmall>();
                Duplicate.NoMatches.AddRange(Duplicate.Items.GetRange(j, Duplicate.Items.Count - j));
                skippedCount = Duplicate.NoMatches.Count;
            }

            double percent = (double)(Duplicate.Items.Count - skippedCount) / Duplicate.Items.Count * 100;
            if (percent >= option.Limit && skippedCount < limitCount)
            {
                NotifyCaller("Match: " + percent + "%", OperationStatus.COMPARING);
                Duplicate.Percentage = percent;
                return true;
            }

            NotifyCaller("Not Match", OperationStatus.COMPARING);
            if (Duplicate.NoMatches != null) Duplicate.NoMatches.Clear();
            return false;
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
                    foreach (var dup in dupList[index].Duplicates)
                    {
                        dup.DupGroup = index;
                    }
                    ++index;
                }
            }
            NotifyCaller("Total: " + dupList.Count + " duplicate groups", OperationStatus.COMPLETE, dupList, total: dupList.Count);
            return dupList;
        }
        #endregion

        public List<DuplicateArchiveInfoList> Search(DuplicateSearchOption option)
        {
            NotifyCaller("Target Count: " + option.Paths.Count, OperationStatus.READY);

            if (option.PreventStanby)
            {
                NotifyCaller("Disabling Sleep", OperationStatus.READY);
                Util.PreventSleep();
            }

            List<FileInfo> fileList =  BuildFileList(option);
            List<DuplicateArchiveInfo> list = CalculateCRC(fileList, option);
            List<DuplicateArchiveInfoList> dupList = BuildDuplicateList(list, option);
            dupList = CleanUpDuplicate(dupList);

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
