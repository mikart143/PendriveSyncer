using PendriveSyncer.Console.Core.Interfaces;
using PendriveSyncer.Console.Core.Model;

namespace PendriveSyncer.Console.Core.Utility
{
    public class StorageDeviceManager
    {
        private readonly IStorageDeviceListener _storageDeviceListener;
        public StorageDeviceManager(IStorageDeviceListener storageDeviceListener)
        {
            _storageDeviceListener = storageDeviceListener;
            _storageDeviceListener.StorageDeviceAddedEvent += StorageDeviceListenerOnStorageDeviceAddedEvent;
            _storageDeviceListener.StorageDeviceRemovedEvent += StorageDeviceListenerOnStorageDeviceRemovedEvent;
            _storageDeviceListener.Start();

        }

        private void StorageDeviceListenerOnStorageDeviceRemovedEvent(object sender, StorageDeviceRemovedEventArgs e)
        {
            System.Console.WriteLine(e.ID);
            System.Console.WriteLine(e.SerialNumber);
        }

        private void StorageDeviceListenerOnStorageDeviceAddedEvent(object sender, StorageDeviceAddedEventArgs e)
        {
            System.Console.WriteLine(e.SerialNumber);
            System.Console.WriteLine(e.ID);
            System.Console.WriteLine(e.Path);
        }
    }
}