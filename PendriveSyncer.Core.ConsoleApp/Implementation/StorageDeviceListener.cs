using System;
using System.Linq;
using System.Management;
using PendriveSyncer.Console.Core.Interfaces;
using PendriveSyncer.Console.Core.Model;

namespace PendriveSyncer.WDA.ConsoleApp.Implementation
{

    public class StorageDeviceListener:IStorageDeviceListener
    {

        public event EventHandler<StorageDeviceAddedEventArgs> StorageDeviceAddedEvent;
        public event EventHandler<StorageDeviceRemovedEventArgs> StorageDeviceRemovedEvent;

        private ManagementEventWatcher _watcherInsert;
        private WqlEventQuery insertQuery = new WqlEventQuery("SELECT * FROM __InstanceCreationEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_DiskDrive'");

        private ManagementEventWatcher _watcherEject;
        private WqlEventQuery ejectQuery = new WqlEventQuery("SELECT * FROM __InstanceDeletionEvent WITHIN 2 WHERE TargetInstance ISA 'Win32_DiskDrive'");
        public void Start()
        {
            _watcherInsert = new ManagementEventWatcher();
            _watcherInsert.EventArrived += WatcherOn_Insert;
            _watcherInsert.Query = insertQuery;
            _watcherInsert.Start();

            _watcherEject = new ManagementEventWatcher();
            _watcherEject.EventArrived += WatcherOn_Eject;
            _watcherEject.Query = ejectQuery;
            _watcherEject.Start();

            FirstScan();

        }

        public void Stop()
        {
            _watcherEject.Stop();
            _watcherInsert.Stop();
        }

        public void Dispose()
        {
            _watcherEject.Dispose();
            _watcherInsert.Dispose();
        }

        private void WatcherOn_Insert(object sender, EventArrivedEventArgs e)
        {
            StorageDeviceAddedEventArgs added = new StorageDeviceAddedEventArgs();

            ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];

            if(instance.Properties["MediaType"].Value == null || instance.Properties["MediaType"].Value.ToString() != "Removable Media") return;

            added.ID = instance.Properties["PNPDeviceID"].Value.ToString();
            added.SerialNumber = GetDeviceSerialNumber(added.ID);
            foreach (ManagementObject partition in new ManagementObjectSearcher(
                "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + instance.Properties["DeviceID"].Value
                                                             + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
            {
                foreach (ManagementObject disk in new ManagementObjectSearcher(
                    "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
                    + partition["DeviceID"]
                    + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                {
                    added.Path = disk["Name"].ToString() + "\\";
                }
            }

            OnStorageDeviceAdded(added);
        }

        private void WatcherOn_Eject(object sender, EventArrivedEventArgs e)
        {
            StorageDeviceRemovedEventArgs removed = new StorageDeviceRemovedEventArgs();
            ManagementBaseObject instance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            removed.ID = instance.Properties["PNPDeviceID"].Value.ToString();
            removed.SerialNumber = GetDeviceSerialNumber(removed.ID);
            OnStorageDeviceRemoved(removed);
        }


        private void OnStorageDeviceAdded(StorageDeviceAddedEventArgs args)
        {
            EventHandler<StorageDeviceAddedEventArgs> handler = StorageDeviceAddedEvent;

            handler?.DynamicInvoke(this, args);
        }

        private void OnStorageDeviceRemoved(StorageDeviceRemovedEventArgs args)
        {
            EventHandler<StorageDeviceRemovedEventArgs> handler = StorageDeviceRemovedEvent;
            handler?.DynamicInvoke(this, args);
            
        }

        private string GetDeviceSerialNumber(string deviceInstanceId)
        {
            var list = deviceInstanceId.Split('\\').ToList();
            return list.Take(list.Count).LastOrDefault();
        }

        private void FirstScan()
        {
            StorageDeviceAddedEventArgs added = new StorageDeviceAddedEventArgs();
            foreach (ManagementObject device in new ManagementObjectSearcher(@"SELECT * FROM Win32_DiskDrive WHERE MediaType LIKE 'Removable Media'").Get())
            {
                added.ID = device.GetPropertyValue("PNPDeviceID").ToString();
                added.SerialNumber = GetDeviceSerialNumber(added.ID);

                foreach (ManagementObject partition in new ManagementObjectSearcher(
                    "ASSOCIATORS OF {Win32_DiskDrive.DeviceID='" + device.Properties["DeviceID"].Value
                                                                 + "'} WHERE AssocClass = Win32_DiskDriveToDiskPartition").Get())
                {
                    foreach (ManagementObject disk in new ManagementObjectSearcher(
                        "ASSOCIATORS OF {Win32_DiskPartition.DeviceID='"
                        + partition["DeviceID"]
                        + "'} WHERE AssocClass = Win32_LogicalDiskToPartition").Get())
                    {

                        added.Path = disk["Name"].ToString() + "\\";
                    }
                }
                OnStorageDeviceAdded(added);
            }
        }
    }
}