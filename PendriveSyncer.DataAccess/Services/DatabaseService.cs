using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PendriveSyncer.DataAccess.DTO;
using PendriveSyncer.DataAccess.Models;

namespace PendriveSyncer.DataAccess.Services
{
    public class DatabaseService
    {
        private readonly DatabaseAccess _databaseAccess;
        public DatabaseService(IDatabaseInit databaseInit)
        {
            _databaseAccess = new DatabaseAccess(databaseInit.GetConnectionString());
        }

        public bool CheckIfDeviceExist(string deviceUniqueId)
        {
            return _databaseAccess.Devices.FindOne(x => x.DeviceIdLine == deviceUniqueId) != null;
        }

        public void InsertNewDevice(DeviceInfo deviceInfo)
        {
            _databaseAccess.Devices.Insert(new Device() {DeviceIdLine = deviceInfo.DeviceUniqueID});
        }

        public List<IgnoredItemInfo> GetIgnoredItems(string deviceUniqueId)
        {
            if(!CheckIfDeviceExist(deviceUniqueId)) return  new List<IgnoredItemInfo>();

            var list = _databaseAccess.Devices.FindOne(x => x.DeviceIdLine == deviceUniqueId).IgnoredItems;

            return list.Any() ? list.Select(x => new IgnoredItemInfo(){PathOfIgnoredItem = x.PathOfIgnoredItem, isFolder = x.isFolder}).ToList() : new List<IgnoredItemInfo>() ;
        }

        public bool HasIgnoredItems(string deviceUniqueId)
        {
            return GetIgnoredItems(deviceUniqueId).Any();
        }
    }
}
