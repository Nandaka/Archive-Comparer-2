using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveComparer2.DB.Model
{
    public class FileEntry
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public string Filename { get; set; }
        public long Size { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public override string ToString()
        {
            return $"Id={Id} - FileEntry={FilePath}\\{Filename} - Size={Size} bytes - LastModified={LastModifiedDate}";
        }

        private Checksum _checksum;
        public Checksum Checksum
        {
            get
            {
                if(_checksum == null)
                {

                }
                return _checksum;
            }
            set
            {
                _checksum = value;
            }
        }
    }
}
