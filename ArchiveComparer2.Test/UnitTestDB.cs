using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ArchiveComparer2.DB;
using System.IO;
using System.Collections.Generic;

namespace ArchiveComparer2.Test
{
    [TestClass]
    public class UnitTestDB
    {
        DataAccess dba = null;

        [TestInitialize]
        public void TestInit()
        {
            if(File.Exists("sqllite.db"))
            {
                File.Delete("sqllite.db");
            }
            if(dba == null) dba = new DataAccess();
        }

        [TestMethod]
        public void TestCreateDB()
        {
            Assert.IsTrue(File.Exists("sqllite.db"));
        }

        [TestMethod]
        public void TestInsertSelectDB()
        {
            var filename = @"..\..\TestFile.txt";
            Assert.IsTrue(File.Exists(filename), $"Test file missing {filename}");
            var fileInfo = new FileInfo(filename);
            {
                var result = dba.InsertFile(fileInfo);
                Assert.IsTrue(result > 0);
            }
            {
                var entry = dba.SelectFile(fileInfo);
                Assert.IsTrue(entry.Filename == fileInfo.Name);
                Assert.IsTrue(entry.FilePath == fileInfo.DirectoryName);
                Console.WriteLine(entry);

                {
                    entry.Checksum = new DB.Model.Checksum()
                    {
                        CRC32="DummyChecksum",
                        MD5="DummyMD5",
                        CRCList="DummyCRClist"
                    };
                    var result = dba.InsertChecksum(entry);
                    Assert.IsTrue(result > 0);
                }
            }

            {
                var entry2 = dba.SelectFile(fileInfo);
                Assert.IsNull(entry2.Checksum);
                Console.WriteLine($"{entry2}");
                entry2 = dba.SelectChecksum(entry2);
                Assert.IsNotNull(entry2.Checksum);
                Assert.IsTrue(entry2.Checksum.CRC32 == "DummyChecksum");
                Assert.IsTrue(entry2.Checksum.MD5 == "DummyMD5");
                Assert.IsTrue(entry2.Checksum.CRCList == "DummyCRClist");
                Console.WriteLine($"{entry2.Checksum}");
            }

            {
                var flist = new List<FileInfo>();
                flist.Add(fileInfo);
                dba.InsertFiles(flist);
                var entry = dba.SelectFile(fileInfo);
                Console.WriteLine($"{entry}");
                var entry2 = dba.SelectChecksum(entry);
                Assert.IsNull(entry2.Checksum);

            }
        }
    }
}
