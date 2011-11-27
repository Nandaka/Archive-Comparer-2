using System;
using System.Collections.Generic;
using System.Text;

using SevenZip;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;

namespace ArchiveComparer2.Library
{
    public struct ArchiveFileInfoSmall
    {
        public string Crc;
        public string Filename;
        public ulong Size;
    }

    public class DuplicateArchiveInfo
    {
        public List<ArchiveFileInfoSmall> Items;
        public string Filename;
        public double Percentage;
        public MatchType MatchType;

        public long ArchivedSize;
        public long RealSize;
        public long FileSize;
        public DateTime CreationTime;

        public int DupGroup;

        public List<ArchiveFileInfoSmall> NoMatches;
        public List<ArchiveFileInfoSmall> Skipped;

        public void SortItems(bool desc=true)
        {
            Items.Sort();
            if (desc) Items.Reverse();
        }

        public override string ToString()
        {
            string str = Filename;
            if (MatchType != MatchType.ORIGINAL)
            {
                str += ", " + MatchType.ToString() + ", FilesCount: " + Items.Count + ", Match: " + Percentage.ToString("0.00") + "%";
            }
            else
            {
                str += ", Original";
            }
            if (NoMatches != null)
            {
                str += ", NoMatchCount: " + NoMatches.Count;
            }
            return str;
        }
    }

    public enum MatchType
    {
        ORIGINAL, EQUALCOUNT, SUBSET
    }

    public class DuplicateArchiveInfoList
    {
        public DuplicateArchiveInfo Original;
        public List<DuplicateArchiveInfo> Duplicates;

        public void SortDuplicates(bool desc = true)
        {
            Duplicates.Sort(new DuplicateArchiveInfoPercentageComparer());
            if (desc) Duplicates.Reverse();
        }
    }

    public class DuplicateArchiveInfoPercentageComparer : IComparer<DuplicateArchiveInfo>
    {
        public int Compare(DuplicateArchiveInfo x, DuplicateArchiveInfo y)
        {
            if (x.Percentage == y.Percentage)
            {
                if (x.Items.Count == y.Items.Count) return 0;
                else if (x.Items.Count > y.Items.Count) return -1;
                else return 1;
            }
            else if (x.Percentage > y.Percentage) return -1;
            else return 1;
        }
    }

    public class DuplicateArchiveInfoItemCountComparer : IComparer<DuplicateArchiveInfo>
    {
        public int Compare(DuplicateArchiveInfo x, DuplicateArchiveInfo y)
        {
            if (x.Items.Count == y.Items.Count) return 0;
            else if (x.Items.Count > y.Items.Count) return -1;
            else return 1;
        }
    }

    public class ArchiveDuplicateDetector
    {
        public delegate void NotifyEventHandler(object sender, NotifyEventArgs e);

        public event NotifyEventHandler Notify;

        private List<DuplicateArchiveInfoList> DupList;

        private void BuildDuplicateList(List<DuplicateArchiveInfo> list, int limit)
        {
            DupList = new List<DuplicateArchiveInfoList>();

            list.Sort(new DuplicateArchiveInfoItemCountComparer());
            
            int totalCount = list.Count;

            while (list.Count > 0)
            {
                
                DuplicateArchiveInfoList dup = new DuplicateArchiveInfoList();
                DuplicateArchiveInfo temp = list[0];
                list.RemoveAt(0);
                dup.Original = temp;

                string message = "Checking: " + temp.Filename + " (" + (totalCount - list.Count) + " of " + totalCount + ")";
                NotifyCaller(message, OperationStatus.BUILDING_DUPLICATE_LIST);

                // check for other possible dups.
                int index = 0;
                while (list.Count > index)
                {
                    DuplicateArchiveInfo curr = list[index];

                    double count = 0;
                    foreach (ArchiveFileInfoSmall crc in curr.Items)
                    {
                        if (IsInside(temp, crc.Crc))
                        {
                            ++count;
                        }
                        else
                        {
                            if (curr.NoMatches == null) curr.NoMatches = new List<ArchiveFileInfoSmall>();
                            curr.NoMatches.Add(crc);
                        }
                    }

                    double tempPercent = count / curr.Items.Count * 100;

                    if (tempPercent >= limit)
                    {
                        curr.Percentage = tempPercent;
                        if (temp.Items.Count == curr.Items.Count) curr.MatchType = MatchType.EQUALCOUNT;
                        else curr.MatchType = MatchType.SUBSET;

                        if (dup.Duplicates == null) dup.Duplicates = new List<DuplicateArchiveInfo>();
                        dup.Duplicates.Add(curr);
                        // remove from the source list.
                        list.Remove(curr);
                    }
                    else
                    {
                        if (curr.NoMatches != null)
                        {
                            curr.NoMatches.Clear();
                        }
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
        }

        private bool IsInside(DuplicateArchiveInfo origin, string crc)
        {
            foreach (var item in origin.Items)
            {
                if (item.Crc == crc) return true;
            }
            return false;
        }

        private void SearchDuplicate(string path, int limit, string filePattern, string blackListPattern)
        {
            Regex re = new Regex(filePattern);
            DirectoryInfo dirList = new DirectoryInfo(path);
            FileInfo[] fileList = dirList.GetFiles("*", SearchOption.AllDirectories);

            List<DuplicateArchiveInfo> list = new List<DuplicateArchiveInfo>();

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

            BuildDuplicateList(list, limit);
        }

        private void CleanUpDuplicate()
        {
            int index = 0;
            while (index < DupList.Count)
            {
                NotifyCaller(" Cleaning " + (index+1) + " of " + DupList.Count, OperationStatus.FILTERING);
                if (DupList[index].Duplicates == null)
                {
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

        public List<DuplicateArchiveInfoList> Search(string path, int limit = 90, string filePattern = ".*", string blackListPattern = "")
        {
            SearchDuplicate(path, limit, filePattern, blackListPattern);
            CleanUpDuplicate();
            return DupList;
        }

        private void SearchThreadingImpl(object parameters)
        {
            Tuple<string, int, string, string> t = (Tuple<string, int, string, string>)parameters;
            Search(t.Item1, t.Item2, t.Item3, t.Item4);
        }

        public void SearchThreading(string path, int limit = 90, string filePattern = ".*", string blackListPattern = "")
        {
            ParameterizedThreadStart ts = new ParameterizedThreadStart(SearchThreadingImpl);
            Thread t = new Thread(ts);
            t.Start(new Tuple<string, int, string, string>(path, limit, filePattern, blackListPattern));
        }
    }

    public class Util
    {
        public static DuplicateArchiveInfo GetArchiveInfo(string filename, string blackListPattern)
        {
            Regex re = new Regex(blackListPattern);
            DuplicateArchiveInfo info = new DuplicateArchiveInfo();
            SevenZipExtractor.SetLibraryPath(@"lib\7z.dll");
            SevenZipExtractor extractor = new SevenZipExtractor(filename);

            info.Filename = filename;
            info.Items = new List<ArchiveFileInfoSmall>();
            info.RealSize = extractor.UnpackedSize;
            info.ArchivedSize = extractor.PackedSize;
                        
            foreach (ArchiveFileInfo af in extractor.ArchiveFileData)
            {
                if (af.IsDirectory) continue;

                ArchiveFileInfoSmall item = new ArchiveFileInfoSmall()
                    {
                        Crc = ConvertToHexString(af.Crc),
                        Filename = af.FileName,
                        Size = af.Size
                    };
                if (!String.IsNullOrWhiteSpace(blackListPattern) && re.IsMatch(af.FileName))
                {
                    if (info.Skipped == null) info.Skipped = new List<ArchiveFileInfoSmall>();
                    info.Skipped.Add(item);
                }
                else
                {
                    info.Items.Add(item);
                }
            }

            extractor.Dispose();

            return info;
        }

        public static string ConvertToHexString(uint value)
        {
            StringBuilder builder = new StringBuilder("");
            builder.Append(Convert.ToString(value, 16).PadLeft(8, '0'));
            return builder.ToString();
        }
    }

    public class NotifyEventArgs : System.EventArgs
    {
        public string Message;
        public OperationStatus Status;
        public List<DuplicateArchiveInfoList> DupList;
    }

    public enum OperationStatus
    {
        READY, BUILDING_FILE_LIST, CALCULATING_CRC, BUILDING_DUPLICATE_LIST, FILTERING, COMPLETE, ERROR
    }
}
