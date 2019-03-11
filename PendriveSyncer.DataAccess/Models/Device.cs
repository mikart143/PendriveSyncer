using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;

namespace PendriveSyncer.DataAccess.Models
{
    public class Device
    {
        [BsonId]
        public int DeviceId { get; set; }
        
        public string DeviceIdLine { get; set; }

        
        public List<IgnoredItem> IgnoredItems { get; set; }
    }
    
}
