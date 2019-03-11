using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
        List<DeviceInformation> deviceInformations = new List<DeviceInformation>();
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
            deviceInformations.Find(x => x.Id == args.Id).Update(args);
        }

        private void WatcherOnStopped(DeviceWatcher sender, object args)
        {
            
        }

        private async void WatcherOnEnumerationCompleted(DeviceWatcher sender, object args)
        {

        }

        private void WatcherOnRemoved(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            deviceInformations.RemoveAll(x => x.Id == args.Id);
        }

        private async void WatcherOnAdded(DeviceWatcher sender, DeviceInformation args)
        {
            deviceInformations.Add(args);
            var DeviceInstanceId = args.Properties["System.Devices.DeviceInstanceId"];

        }
    }
}
