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
    public enum MatchType
    {
        ORIGINAL, EQUALCOUNT, SUBSET
    }

    public enum OperationStatus
    {
        READY, BUILDING_FILE_LIST, CALCULATING_CRC, BUILDING_DUPLICATE_LIST, COMPARING, FILTERING, COMPLETE, ERROR
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

        public string ToCRCString()
        {
            string crc = "";
            foreach (var item in Items)
            {
                crc += item.Crc;
            }
            return crc;
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

    
    public class Util
    {
        private static ILog Logger = LogManager.GetLogger(typeof(Util));

        /// <summary>
        /// Build DuplicateArchiveInfo containing the files' crc, sorted.
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="blackListPattern"></param>
        /// <returns></returns>
        public static DuplicateArchiveInfo GetArchiveInfo(string filename, string blackListPattern)
        {
            Logger.Info(filename);

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
                    Logger.Info("Skipping: " + af.FileName);
                }
                else
                {
                    info.Items.Add(item);
                }
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
    }

    
}
