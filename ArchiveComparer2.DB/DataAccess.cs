using ArchiveComparer2.DB.Model;
using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Diagnostics;
using System.Reflection;

namespace ArchiveComparer2.DB
{
    public class DataAccess
    {
        private readonly string _connStr;

        public static DataAccess DB = new DataAccess();

        public DataAccess(string newDbPath = "sqllite.db")
        {
            if (string.IsNullOrWhiteSpace(newDbPath))
            {
                newDbPath = "sqllite.db";
            }
            this._connStr = "Cache=Shared;Pooling=True;Data Source=" + newDbPath;

            if (!File.Exists(newDbPath))
            {
                File.WriteAllBytes(newDbPath, new byte[0]);
            }

            // create table
            CreateTableFiles();
            CreateTableChecksums();

        }

        #region Create tables
        private readonly string CREATE_FILES_SQL = $@"
CREATE TABLE IF NOT EXISTS `files` (
    `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
    `filepath` TEXT NOT NULL,
    `filename` TEXT NOT NULL,
    `size` INTEGER NOT NULL,
    `create_date` INTEGER NOT NULL,
    `last_modified_date` INTEGER NOT NULL,
    `update_date` INTEGER NOT NULL DEFAULT CURRENT_TIMESTAMP
)
";

        private readonly string INDEX_FILES_SQL = @"
CREATE UNIQUE INDEX IF NOT EXISTS `files_idx1` ON `files` (
    `filepath`,
    `filename`
)
";
        private bool CreateTableFiles()
        {
            var result = -1;
            using (var connection = new SQLiteConnection(_connStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = CREATE_FILES_SQL;
                result = cmd.ExecuteNonQuery();
                cmd.CommandText = INDEX_FILES_SQL;
                result = cmd.ExecuteNonQuery();
            }

            return result > -1 ? true : false;
        }

        private readonly string CREATE_CHECKSUM_SQL = $@"
CREATE TABLE IF NOT EXISTS `checksums` (
    `id` INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT UNIQUE,
    `crc32` TEXT NULL,
    `md5` TEXT NULL,
    `crc_list` TEXT NULL,
    `update_date` INTEGER NOT NULL DEFAULT CURRENT_TIMESTAMP,
    `file_id` INTEGER NOT NULL,
    FOREIGN KEY(`file_id`) REFERENCES `files`(`id`) ON DELETE CASCADE
)
";

        private bool CreateTableChecksums()
        {
            var result = -1;
            using (var connection = new SQLiteConnection(_connStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = CREATE_CHECKSUM_SQL;
                result = cmd.ExecuteNonQuery();
            }

            return result > -1 ? true : false;
        }
        #endregion

        #region files table

        private readonly string INSERT_FILES_SQL = @"
INSERT INTO files (filepath, filename, size, create_date, last_modified_date)
VALUES (@filepath, @filename, @size, @create_date, @last_modified_date)
";

        public int InsertFile(FileInfo fileInfo)
        {
            var result = -1;
            using (var connection = new SQLiteConnection(_connStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = INSERT_FILES_SQL;

                cmd.Parameters.Add(new SQLiteParameter("@filepath", fileInfo.DirectoryName));
                cmd.Parameters.Add(new SQLiteParameter("@filename", fileInfo.Name));
                cmd.Parameters.Add(new SQLiteParameter("@size", fileInfo.Length));
                cmd.Parameters.Add(new SQLiteParameter("@create_date", fileInfo.CreationTimeUtc));
                cmd.Parameters.Add(new SQLiteParameter("@last_modified_date", fileInfo.LastWriteTimeUtc));
                result = cmd.ExecuteNonQuery();

                // TODO: potential integer overflow
                if(result > 0) return (int) connection.LastInsertRowId;
            }

            return result;
        }

        private readonly string SELECT_FILES_SQL = @"
SELECT * FROM files
WHERE filepath = @filepath and filename = @filename
";
        public FileEntry SelectFile(FileInfo fileInfo)
        {
            FileEntry result = null;
            using (var connection = new SQLiteConnection(_connStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = SELECT_FILES_SQL;

                cmd.Parameters.Add(new SQLiteParameter("@filepath", fileInfo.DirectoryName));
                cmd.Parameters.Add(new SQLiteParameter("@filename", fileInfo.Name));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result = new FileEntry()
                    {
                        Id = reader.GetInt32(0),
                        FilePath = reader.GetString(1),
                        Filename = reader.GetString(2),
                        Size = reader.GetInt64(3),
                        CreateDate = reader.GetDateTime(4),
                        LastModifiedDate = reader.GetDateTime(5),
                        UpdateDate = reader.GetDateTime(6)
                    };
                    break;
                }
            }
            return result;
        }

        #endregion

        #region checksum
        private readonly string INSERT_CHECKSUM_SQL = @"
INSERT INTO checksums (crc32, md5, crc_list, file_id)
VALUES (@crc32, @md5, @crc_list, @file_id)
";
        public int InsertChecksum(FileEntry entry)
        {
            if(entry == null || entry.Id <= 0)
            {
                throw new Exception($"Invalid file entry= {entry}");
            }

            var result = -1;
            using (var connection = new SQLiteConnection(_connStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = INSERT_CHECKSUM_SQL;

                cmd.Parameters.Add(new SQLiteParameter("@crc32", entry.Checksum.CRC32));
                cmd.Parameters.Add(new SQLiteParameter("@md5", entry.Checksum.MD5));
                cmd.Parameters.Add(new SQLiteParameter("@crc_list", entry.Checksum.CRCList));
                cmd.Parameters.Add(new SQLiteParameter("@file_id", entry.Id));
                result = cmd.ExecuteNonQuery();

                // TODO: potential integer overflow
                if (result > 0) return (int)connection.LastInsertRowId;
            }

            return result;
        }

        private readonly string SELECT_CHECKSUM_SQL = @"
SELECT * FROM checksums
WHERE file_id = @file_id
";

        public FileEntry SelectChecksum(FileEntry entry)
        {
            using (var connection = new SQLiteConnection(_connStr))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = SELECT_CHECKSUM_SQL;

                cmd.Parameters.Add(new SQLiteParameter("@file_id", entry.Id));
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var checksum = new Checksum()
                    {
                        Id = reader.GetInt32(0),
                        CRC32 = reader.GetString(1),
                        MD5 = reader.GetString(2),
                        CRCList = reader.GetString(3),
                        UpdateDate = reader.GetDateTime(4),
                        FileId = reader.GetInt32(5)
                    };
                    entry.Checksum = checksum;
                    break;
                }
            }
            return entry;
        }
        #endregion

        private readonly string DELETE_FILES_SQL = @"
DELETE FROM files
WHERE id = @id
";
        private readonly string PRAGMA_FK_CHECK = @"PRAGMA foreign_keys = ON";
        public int InsertFiles(List<FileInfo> files)
        {
            var result = 0;
            using (var connection = new SQLiteConnection(_connStr))
            {
                connection.Open();

                // enable foreign key check
                var cmdPragma = connection.CreateCommand();
                cmdPragma.CommandText = PRAGMA_FK_CHECK;
                cmdPragma.ExecuteNonQuery();

                foreach (var f in files)
                {
                    // check if exists
                    var existingFile = GetExistingFile(connection, f);

                    if(existingFile != null)
                    {
                        if(existingFile.Size == f.Length &&
                           existingFile.CreateDate == f.CreationTimeUtc &&
                           existingFile.LastModifiedDate == f.LastAccessTimeUtc)
                        {
                            continue;
                        }

                        // delete old data if different size or timestamp
                        var cmdDel = connection.CreateCommand();
                        cmdDel.CommandText = DELETE_FILES_SQL;
                        cmdDel.Parameters.Add(new SQLiteParameter("@id", existingFile.Id));
                        var delResult = cmdDel.ExecuteNonQuery();
                        if (delResult == 0) throw new Exception($"Expected to return > 0 when deleting {existingFile}");
                    }

                    // else always replace the old data
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = INSERT_FILES_SQL;

                    cmd.Parameters.Add(new SQLiteParameter("@filepath", f.DirectoryName));
                    cmd.Parameters.Add(new SQLiteParameter("@filename", f.Name));
                    cmd.Parameters.Add(new SQLiteParameter("@size", f.Length));
                    cmd.Parameters.Add(new SQLiteParameter("@create_date", f.CreationTimeUtc));
                    cmd.Parameters.Add(new SQLiteParameter("@last_modified_date", f.LastWriteTimeUtc));
                    result += cmd.ExecuteNonQuery();
                }
            }
            return -1;
        }

        private FileEntry GetExistingFile(SQLiteConnection connection, FileInfo f)
        {
            FileEntry existingEntry = null;
            var cmdCheck = connection.CreateCommand();
            cmdCheck.CommandText = SELECT_FILES_SQL;
            cmdCheck.Parameters.Add(new SQLiteParameter("@filepath", f.DirectoryName));
            cmdCheck.Parameters.Add(new SQLiteParameter("@filename", f.Name));
            var reader = cmdCheck.ExecuteReader();
            while (reader.Read())
            {
                existingEntry = new FileEntry()
                {
                    Id = reader.GetInt32(0),
                    FilePath = reader.GetString(1),
                    Filename = reader.GetString(2),
                    Size = reader.GetInt64(3),
                    CreateDate = reader.GetDateTime(4),
                    LastModifiedDate = reader.GetDateTime(5),
                    UpdateDate = reader.GetDateTime(6)
                };
                break;
            }
            return existingEntry;
        }
    }
}
