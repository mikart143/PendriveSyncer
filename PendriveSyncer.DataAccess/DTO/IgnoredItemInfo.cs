using System;
using System.Collections.Generic;
using System.Text;

namespace PendriveSyncer.DataAccess.DTO
{
    public class IgnoredItemInfo
    {
        public string PathOfIgnoredItem { get; set; }
        public bool isFolder { get; set; }
    }
}
