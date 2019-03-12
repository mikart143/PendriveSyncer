using System;
using System.Collections.Generic;
using System.Security.Authentication;
using System.Text;
using LiteDB;

namespace PendriveSyncer.DataAccess.Models
{
    public class FileInfo
    {
        public int FileInfoId { get; set; }

        public int HashType { get; set; }

        public string LocalFileHash { get; set; }

        public string RemoteFileHash { get; set; }

        public string FileName { get; set; }

        [BsonRef("device")]
        public Device Device { get; set; }
    }
}
