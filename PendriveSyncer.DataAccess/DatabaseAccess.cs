using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using PendriveSyncer.DataAccess.Models;

namespace PendriveSyncer.DataAccess
{
    public class DatabaseAccess
    {
        private LiteDatabase _db = null;
        private LiteCollection<Device> _devices = null;
        private LiteCollection<IgnoredItem> _ignoredItems = null;

        public LiteCollection<Device> Devices
        {
            get { return _devices.Include(x => x.IgnoredItems); }
            set { _devices = value; }
        }

        public LiteCollection<IgnoredItem> IgnoredItems
        {
            get { return _ignoredItems.Include(x => x.Device); }
            set { _ignoredItems = value; }
        }
        protected internal DatabaseAccess(string databaseStringConnection)
        {
            _db = new LiteDatabase(databaseStringConnection);
            _devices = _db.GetCollection<Device>("device");
            _ignoredItems = _db.GetCollection<IgnoredItem>("ignoreditem");
            BsonMapper.Global.Entity<Device>().DbRef(x => x.IgnoredItems, "ignoreditem");
            

        }
    }
}
