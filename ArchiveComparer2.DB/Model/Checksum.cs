using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArchiveComparer2.DB.Model
{
    public class Checksum
    {
        public int Id { get; set; }
        public string CRC32 { get; set; }
        public string MD5 { get; set; }
        public string CRCList { get; set; }
        public DateTime UpdateDate { get; set; }

        public override string ToString()
        {
            return $"Id={Id} - CRC32={CRC32} - MD5={MD5} bytes - UpdateDate={UpdateDate} - CRCList={CRCList}";
        }
    }
}
