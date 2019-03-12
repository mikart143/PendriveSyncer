using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;
using Windows.Devices.Usb;
using Windows.UI.Xaml;

namespace Pendrive.Syncer.UWP.ConsoleApp.Utility
{
    class DevicesControler
    {
        public static DeviceWatcher watcher = null;
        private List<DeviceInformation> _deviceInformations = new List<DeviceInformation>();
        public void StartWatcher()
        {
            
            watcher = DeviceInformation.CreateWatcher(DeviceInformation.GetAqsFilterFromDeviceClass(DeviceClass.PortableStorageDevice), new string[]{ "System.Devices.DeviceInstanceId" });
            watcher.Added += WatcherOnAdded;
            watcher.Removed += WatcherOnRemoved;
            watcher.EnumerationCompleted += WatcherOnEnumerationCompleted;
            watcher.Stopped += WatcherOnStopped;
            watcher.Updated += WatcherOnUpdated;
            watcher.Start();
        }

        private void WatcherOnUpdated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            _deviceInformations.Find(x => x.Id == args.Id).Update(args);
        }

        private void WatcherOnStopped(DeviceWatcher sender, object args)
        {
            
        }

        private void WatcherOnEnumerationCompleted(DeviceWatcher sender, object args)
        {

        }

        private void WatcherOnRemoved(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            _deviceInformations.RemoveAll(x => x.Id == args.Id);
        }

        private  void WatcherOnAdded(DeviceWatcher sender, DeviceInformation args)
        {
            _deviceInformations.Add(args);
            string deviceInstanceId = args.Properties["System.Devices.DeviceInstanceId"] as string;
            string sn = GetDeviceSerialNumber(deviceInstanceId);


        }

        private string GetDeviceSerialNumber(string deviceInstanceId)
        {
            var list = deviceInstanceId.Split("#").ToList();
            return list.Take(list.Count - 1).LastOrDefault();
        }
    }
}
