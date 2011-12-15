using System;
using System.Collections.Generic;
using System.Text;

using SevenZip;
using System.Text.RegularExpressions;


namespace ArchiveComparer2.Library
{
    public enum MatchType
    {
        ORIGINAL, EQUALCOUNT, SUBSET
    }

    public enum OperationStatus
    {
        READY, BUILDING_FILE_LIST, CALCULATING_CRC, BUILDING_DUPLICATE_LIST, COMPARING, FILTERING, COMPLETE, ERROR, PAUSED, RESUMED, STOPPED
    }

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
            Items.Sort(new ArchiveFileInfoSmallCRCComparer());
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

        private string _crcString;
        public string ToCRCString()
        {
            if (String.IsNullOrEmpty(_crcString))
            {
                _crcString = "";
                foreach (var item in Items)
                {
                    _crcString += item.Crc;
                }
            }
            return _crcString;
        }
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

    public class DuplicateSearchOption
    {
        public List<string> Paths;
        public int Limit = 90;
        public string FilePattern = ".*";
        public string BlacklistPattern = "";
        public int IgnoreLimit = 5;

        public string SevenZipPath = @"lib\7z.dll";
        public bool BlacklistCaseInsensitive = false;
        public bool FileCaseInsensitive = false;

        public bool OnlyPerfectMatch = false;
    }


    public class ArchiveFileInfoSmallCRCComparer : IComparer<ArchiveFileInfoSmall>
    {
        public int Compare(ArchiveFileInfoSmall x, ArchiveFileInfoSmall y)
        {
            return String.Compare(x.Crc, y.Crc);
        }
    }

    public class DuplicateArchiveInfoPercentageComparer : IComparer<DuplicateArchiveInfo>
    {
        public int Compare(DuplicateArchiveInfo x, DuplicateArchiveInfo y)
        {
            int result = ComparePercentage(x, y);
            if (result == 0) result = CompareItemCount(x, y);
            if (result == 0) result = CompareFileSize(x, y);
            return result;
        }

        private int ComparePercentage(DuplicateArchiveInfo x, DuplicateArchiveInfo y)
        {
            if (x.Percentage == y.Percentage) return 0;
            else if (x.Percentage > y.Percentage) return -1;
            else return 1;
        }

        private int CompareItemCount(DuplicateArchiveInfo x, DuplicateArchiveInfo y)
        {
            if (x.Items.Count == y.Items.Count) return 0;
            else if (x.Items.Count > y.Items.Count) return -1;
            else return 1;
        }

        private int CompareFileSize(DuplicateArchiveInfo x, DuplicateArchiveInfo y)
        {
            if (x.FileSize == y.FileSize) return 0;
            else if (x.FileSize > y.FileSize) return -1;
            else return 1;
        }
    }

    public class DuplicateArchiveInfoItemCountComparer : IComparer<DuplicateArchiveInfo>
    {
        public int Compare(DuplicateArchiveInfo x, DuplicateArchiveInfo y)
        {
            int result = CompareItemCount(x, y);
            if (result == 0) result = CompareFileSize(x, y);
            return result;
        }
        
        private int CompareItemCount(DuplicateArchiveInfo x, DuplicateArchiveInfo y)
        {
            if (x.Items.Count == y.Items.Count) return 0;
            else if (x.Items.Count > y.Items.Count) return -1;
            else return 1;
        }

        private int CompareFileSize(DuplicateArchiveInfo x, DuplicateArchiveInfo y)
        {
            if (x.FileSize == y.FileSize) return 0;
            else if (x.FileSize > y.FileSize) return -1;
            else return 1;
        }
    }

    
    public class Util
    {
        /// <summary>
        /// Build DuplicateArchiveInfo containing the files' crc, sorted.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="blackListPattern"></param>
        /// <returns></returns>
        public static DuplicateArchiveInfo GetArchiveInfo(string filename, string blackListPattern, bool caseInsensitive, string sevenZipPath)
        {
            Regex re = new Regex(blackListPattern, caseInsensitive ? RegexOptions.IgnoreCase : RegexOptions.None);
            DuplicateArchiveInfo info = new DuplicateArchiveInfo();
            SevenZipExtractor.SetLibraryPath(sevenZipPath);
            SevenZipExtractor extractor = new SevenZipExtractor(filename);

            info.Filename = filename;
            info.Items = new List<ArchiveFileInfoSmall>();
            info.RealSize = extractor.UnpackedSize;
            info.ArchivedSize = extractor.PackedSize;

            ulong countedSize = 0;

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
                countedSize += af.Size;
            }

            if (info.RealSize == -1)
            {
                info.RealSize = Convert.ToInt64(countedSize);
            }

            info.SortItems();

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
        public int TotalCount;
        public int CurrentCount;
    }
}
