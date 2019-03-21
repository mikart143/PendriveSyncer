using System;
using PendriveSyncer.Console.Core.Model;

namespace PendriveSyncer.Console.Core.Interfaces
{
    public interface IStorageDeviceListener
    {
        event EventHandler<StorageDeviceAddedEventArgs> StorageDeviceAddedEvent;
        event EventHandler<StorageDeviceRemovedEventArgs> StorageDeviceRemovedEvent;

        void Start();
        void Stop();
        void Dispose();
    }
}