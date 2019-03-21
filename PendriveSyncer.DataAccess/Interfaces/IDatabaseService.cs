using System.Collections.Generic;
using PendriveSyncer.DataAccess.DTO;

namespace PendriveSyncer.DataAccess
{
    public interface IDatabaseService
    {
        bool CheckIfDeviceExist(string deviceUniqueId);
        void InsertNewDevice(DeviceInfo deviceInfo);
        List<IgnoredItemInfo> GetIgnoredItems(string deviceUniqueId);
        bool HasIgnoredItems(string deviceUniqueId);
    }
}