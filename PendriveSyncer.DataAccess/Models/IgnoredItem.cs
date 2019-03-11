using LiteDB;

namespace PendriveSyncer.DataAccess.Models
{
    public class IgnoredItem
    {
        [BsonId]
        public int IgnoredId { get; set; }
        public  string PathOfIgnoredItem { get; set; }
        public  bool isFolder { get; set; }

        [BsonRef("device")]
        public Device Device { get; set; }
    }
}