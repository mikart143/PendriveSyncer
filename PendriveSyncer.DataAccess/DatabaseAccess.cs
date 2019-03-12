using LiteDB;
using PendriveSyncer.DataAccess.Models;
using FileInfo = PendriveSyncer.DataAccess.Models.FileInfo;

namespace PendriveSyncer.DataAccess
{
    public class DatabaseAccess
    {
        private LiteDatabase _db = null;


        private LiteCollection<Device> _devices = null;
        public LiteCollection<Device> Devices
        {
            get { return _devices.Include(x => x.IgnoredItems); }
            set { _devices = value; }
        }

        private LiteCollection<IgnoredItem> _ignoredItems = null;
        public LiteCollection<IgnoredItem> IgnoredItems
        {
            get { return _ignoredItems.Include(x => x.Device); }
            set { _ignoredItems = value; }
        }

        private LiteCollection<FileInfo> _fileInfos = null;
        public LiteCollection<FileInfo> FileInfos
        {
            get { return _fileInfos.Include(x => x.Device); }
            set { _fileInfos = value; }
        }
        protected internal DatabaseAccess(string databaseStringConnection)
        {
            _db = new LiteDatabase(databaseStringConnection);
            _devices = _db.GetCollection<Device>("device");
            _ignoredItems = _db.GetCollection<IgnoredItem>("ignoreditem");
            _fileInfos = _db.GetCollection<FileInfo>("fileinfo");
            BsonMapper.Global.Entity<Device>().DbRef(x => x.IgnoredItems, "ignoreditem");
        }
    }
}
