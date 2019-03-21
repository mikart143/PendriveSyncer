using System;

namespace PendriveSyncer.Console.Core.Model
{
    public class StorageDeviceAddedEventArgs:EventArgs
    {
        public string ID { get; set; }
        public string SerialNumber { get; set; }
        public string Path { get; set; }
    }
}