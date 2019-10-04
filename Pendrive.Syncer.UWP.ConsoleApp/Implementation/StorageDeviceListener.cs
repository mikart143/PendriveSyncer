using System;
using System.Linq;
using System.Runtime.InteropServices;
using Windows.Devices.Enumeration;
using Windows.Foundation.Metadata;
using PendriveSyncer.Console.Core.Interfaces;
using PendriveSyncer.Console.Core.Model;

namespace Pendrive.Syncer.UWP.ConsoleApp.Implementation
{
    public class StorageDeviceListener:IStorageDeviceListener
    {
        public event EventHandler<StorageDeviceAddedEventArgs> StorageDeviceAddedEvent;
        public event EventHandler<StorageDeviceRemovedEventArgs> StorageDeviceRemovedEvent; 
        private static DeviceWatcher _watcher = null;
        public void Start()
        {
            _watcher = _watcher = DeviceInformation.CreateWatcher(DeviceInformation.GetAqsFilterFromDeviceClass(DeviceClass.PortableStorageDevice), new string[] { "System.Devices.DeviceInstanceId" });
            _watcher.Added += WatcherOnAdded;
            _watcher.Removed += WatcherOnRemoved;
            _watcher.Start();
        }

        public void Stop()
        {
            _watcher.Stop();
        }

        public void Dispose()
        {
          
        }

        private void WatcherOnRemoved(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            OnStorageDeviceRemoved(new StorageDeviceRemovedEventArgs()
            {
                ID = args.Id,
                SerialNumber = GetDeviceSerialNumber(args.Id)
            });
        }

        private void WatcherOnAdded(DeviceWatcher sender, DeviceInformation args)
        {
            OnStorageDeviceAdded(new StorageDeviceAddedEventArgs()
            {
                ID = args.Id,
                SerialNumber = GetDeviceSerialNumber(args.Id),
                Path = Windows.Devices.Portable.StorageDevice.FromId(args.Id).Path
            });
        }

        private void OnStorageDeviceAdded(StorageDeviceAddedEventArgs args)
        {
            EventHandler<StorageDeviceAddedEventArgs> handler = StorageDeviceAddedEvent;

            handler?.Invoke(this, args);
        }

        private void OnStorageDeviceRemoved(StorageDeviceRemovedEventArgs args)
        {
            EventHandler<StorageDeviceRemovedEventArgs> handler = StorageDeviceRemovedEvent;
            handler?.Invoke(this, args);
        }

        private string GetDeviceSerialNumber(string deviceInstanceId)
        {
            var list = deviceInstanceId.Split("#").ToList();
            return list.Take(list.Count - 2).LastOrDefault();
        }
    }
}