using System;
using System.Collections.Generic;
using System.Text;

namespace PendriveSyncer.FileManagment.DTO
{
    public class FileInformation
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public string Hash { get; set; }
        public DateTime LastModification { get; set; }
    }
}
