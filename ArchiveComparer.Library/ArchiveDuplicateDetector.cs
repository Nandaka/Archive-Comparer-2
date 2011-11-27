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

        private void SearchDuplicate(string path, int limit, string filePattern, string blackListPattern)
        {
            NotifyCaller("Building file list.", OperationStatus.BUILDING_FILE_LIST);

            Regex re = new Regex(filePattern);
            DirectoryInfo dirList = new DirectoryInfo(path);
            FileInfo[] fileList = dirList.GetFiles("*", SearchOption.AllDirectories);

            List<DuplicateArchiveInfo> list = new List<DuplicateArchiveInfo>();

            NotifyCaller("Total File: "+ fileList.Length, OperationStatus.BUILDING_FILE_LIST);

            foreach (FileInfo f in fileList)
            {
                if (!re.IsMatch(f.Name)) continue;
                NotifyCaller(f.FullName, OperationStatus.CALCULATING_CRC);
                try
                {
                    DuplicateArchiveInfo item = Util.GetArchiveInfo(f.FullName, blackListPattern);
                    item.FileSize = f.Length;
                    item.CreationTime = f.CreationTime;

                    list.Add(item);
                }
                catch (Exception ex)
                {
                    string message = ex.Message + " (" + f.FullName + ")";
                    NotifyCaller(message, OperationStatus.ERROR);
                }
            }

            NotifyCaller("Complete calculating CRC, total: " + list.Count, OperationStatus.CALCULATING_CRC);

            BuildDuplicateList(list, limit);
        }

        private void BuildDuplicateList(List<DuplicateArchiveInfo> list, int limit)
        {
            NotifyCaller("Start building duplicate list.", OperationStatus.BUILDING_DUPLICATE_LIST);

            DupList = new List<DuplicateArchiveInfoList>();

            list.Sort(new DuplicateArchiveInfoItemCountComparer());

            //int totalCount = list.Count;
            int i = 0;
            while (list.Count > 0)
            {
                ++i;
                DuplicateArchiveInfoList dup = new DuplicateArchiveInfoList();
                DuplicateArchiveInfo temp = list[0];
                list.RemoveAt(0);
                dup.Original = temp;

                string message = "Checking: " + temp.Filename + " ( Duplicate group found: " + i + " File to check left: " + list.Count + ")";
                NotifyCaller(message, OperationStatus.BUILDING_DUPLICATE_LIST);

                // check for other possible dups.
                int index = 0;
                while (list.Count > index)
                {
                    DuplicateArchiveInfo curr = list[index];

                    if (Compare(ref temp, ref curr, limit))
                    {
                        if (dup.Duplicates == null) dup.Duplicates = new List<DuplicateArchiveInfo>();
                        dup.Duplicates.Add(curr);
                        // remove from the source list.
                        list.Remove(curr);
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

        private bool Compare(ref DuplicateArchiveInfo Origin, ref DuplicateArchiveInfo Duplicate, int limit)
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

            int limitCount = Duplicate.Items.Count - (Duplicate.Items.Count * limit / 100);
            int skippedCount = 0;
            int i = 0;
            int j = 0;
            while (i < Origin.Items.Count && j < Duplicate.Items.Count && skippedCount < limitCount)
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
            NotifyCaller("Total: " + DupList.Count, OperationStatus.COMPLETE, DupList);
        }

        private void NotifyCaller(string message, OperationStatus status, List<DuplicateArchiveInfoList> dupList = null)
        {
            if (Notify != null)
            {
                Notify(this, new NotifyEventArgs() { Message = message, Status = status, DupList = dupList });
            }
        }

        private void SearchThreadingImpl(object parameters)
        {
            Tuple<string, int, string, string> t = (Tuple<string, int, string, string>)parameters;
            Search(t.Item1, t.Item2, t.Item3, t.Item4);
        }


        public List<DuplicateArchiveInfoList> Search(string path, int limit = 90, string filePattern = ".*", string blackListPattern = "")
        {
            NotifyCaller("Target: " + path, OperationStatus.READY);
            SearchDuplicate(path, limit, filePattern, blackListPattern);
            CleanUpDuplicate();
            return DupList;
        }

        public void SearchThreading(string path, int limit = 90, string filePattern = ".*", string blackListPattern = "")
        {
            ParameterizedThreadStart ts = new ParameterizedThreadStart(SearchThreadingImpl);
            Thread t = new Thread(ts);
            t.Start(new Tuple<string, int, string, string>(path, limit, filePattern, blackListPattern));
        }
    }

}
