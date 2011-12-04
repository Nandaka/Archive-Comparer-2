using System;
using System.Collections.Generic;
using System.Text;

using SevenZip;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using log4net;

namespace ArchiveComparer2.Library
{

    public class ArchiveDuplicateDetector
    {
        public delegate void NotifyEventHandler(object sender, NotifyEventArgs e);
        public event NotifyEventHandler Notify;
        private List<DuplicateArchiveInfoList> DupList;

        private ManualResetEvent _shutdownEvent = new ManualResetEvent(false);
        private ManualResetEvent _pauseEvent = new ManualResetEvent(true);
        private Thread _thread;
        

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

            if (_thread != null)
            {
                // Wait for the thread to exit
                _thread.Join();
            }
            NotifyCaller("Stopped", OperationStatus.STOPPED);
        }
        

        // step 1 - build file list
        private void SearchDuplicate(DuplicateSearchOption option)
        {
            NotifyCaller("Building file list.", OperationStatus.BUILDING_FILE_LIST);

            Regex re = new Regex(option.FilePattern);
            DirectoryInfo dirList = new DirectoryInfo(option.Path);
            FileInfo[] fileList = dirList.GetFiles("*", SearchOption.AllDirectories);

            List<DuplicateArchiveInfo> list = new List<DuplicateArchiveInfo>();

            NotifyCaller("Total File: " + fileList.Length, OperationStatus.BUILDING_FILE_LIST, total:fileList.Length);

            int i = 0;
            foreach (FileInfo f in fileList)
            {
                _pauseEvent.WaitOne(Timeout.Infinite);

                if (_shutdownEvent.WaitOne(0))
                    break;

                if (!re.IsMatch(f.Name)) continue;
                NotifyCaller(f.FullName, OperationStatus.CALCULATING_CRC, curr:i, total:fileList.Length);
                try
                {
                    DuplicateArchiveInfo item = Util.GetArchiveInfo(f.FullName, option.BlacklistPattern);
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

            BuildDuplicateList(list, option.Limit, option.IgnoreLimit);
        }

        private void BuildDuplicateList(List<DuplicateArchiveInfo> list, int limit, int ignoreLimit)
        {
            NotifyCaller("Start building duplicate list.", OperationStatus.BUILDING_DUPLICATE_LIST);

            DupList = new List<DuplicateArchiveInfoList>();

            list.Sort(new DuplicateArchiveInfoItemCountComparer());

            int totalCount = list.Count;
            int i = 0;
            while (list.Count > 0)
            {
                _pauseEvent.WaitOne(Timeout.Infinite);

                if (_shutdownEvent.WaitOne(0))
                    break;

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

                    if (Compare(ref temp, ref curr, limit, ignoreLimit))
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

                DupList.Add(dup);
            }

            foreach (DuplicateArchiveInfoList dup in DupList)
            {
                if (dup.Duplicates != null)
                {
                    dup.Duplicates.Sort(new DuplicateArchiveInfoPercentageComparer());
                }
            }
            NotifyCaller("Building Duplicate List Complete.", OperationStatus.BUILDING_DUPLICATE_LIST);
        }

        private bool Compare(ref DuplicateArchiveInfo Origin, ref DuplicateArchiveInfo Duplicate, int limit, int ignoreLimit)
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
            }
            else
            {
                Duplicate.MatchType = MatchType.SUBSET;
            }

            // Check each files in duplicate
            int limitCount;
            if (ignoreLimit > Duplicate.Items.Count) limitCount = 0;
            else limitCount = Duplicate.Items.Count - (Duplicate.Items.Count * limit / 100);

            int skippedCount = 0;
            int i = 0;
            int j = 0;
            while (i < Origin.Items.Count && j < Duplicate.Items.Count && skippedCount <= limitCount)
            {
                int result = String.Compare(Origin.Items[i].Crc, Duplicate.Items[j].Crc);
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

            double percent = (double)(Duplicate.Items.Count - skippedCount) / Duplicate.Items.Count * 100;
            if (percent >= limit && skippedCount < limitCount)
            {
                NotifyCaller("Match: " + percent + "%", OperationStatus.COMPARING);
                Duplicate.Percentage = percent;
                return true;
            }
            else
            {
                NotifyCaller("Not Match", OperationStatus.COMPARING);
                if (Duplicate.NoMatches != null) Duplicate.NoMatches.Clear();
                return false;
            }
        }
                

        private void CleanUpDuplicate()
        {
            int index = 0;
            while (index < DupList.Count)
            {
                NotifyCaller(" Cleaning " + (index + 1) + " of " + DupList.Count, OperationStatus.FILTERING);
                if (DupList[index].Duplicates == null)
                {
                    NotifyCaller("Removing: " + DupList[index].Original.Filename, OperationStatus.FILTERING);
                    DupList.RemoveAt(index);
                }
                else
                {
                    DupList[index].Original.DupGroup = index;
                    foreach (var dup in DupList[index].Duplicates)
                    {
                        dup.DupGroup = index;
                    }
                    ++index;
                }
            }
            NotifyCaller("Total: " + DupList.Count + " duplicate groups", OperationStatus.COMPLETE, DupList, total:DupList.Count);
        }

        private void NotifyCaller(string message, OperationStatus status, List<DuplicateArchiveInfoList> dupList = null, int curr = 0, int total = 0)
        {
            if (Notify != null)
            {
                Notify(this, new NotifyEventArgs() { Message = message, Status = status, DupList = dupList, TotalCount = total, CurrentCount = curr });
            }
        }

        private void SearchThreadingImpl(object option)
        {
            
            Search((DuplicateSearchOption) option);
        }


        public List<DuplicateArchiveInfoList> Search(DuplicateSearchOption option)
        {
            NotifyCaller("Target: " + option.Path, OperationStatus.READY);
            SearchDuplicate(option);
            CleanUpDuplicate();
            return DupList;
        }

        public void SearchThreading(DuplicateSearchOption option)
        {
            ParameterizedThreadStart ts = new ParameterizedThreadStart(SearchThreadingImpl);
            if (_thread == null || _thread.ThreadState == ThreadState.Stopped)
            {
                _thread = new Thread(ts);
                _thread.Start(option);
            }
        }
    }

}
