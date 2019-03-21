using LiteDB;
using PendriveSyncer.DataAccess.Models;

namespace PendriveSyncer.DataAccess.Interfaces
{
    public interface IDatabaseAccess
    {
        void CreateInstance();
        LiteCollection<Device> Devices { get; set; }
        LiteCollection<IgnoredItem> IgnoredItems { get; set; }
        LiteCollection<FileInfo> FileInfos { get; set; }
    }
}