using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using PendriveSyncer.Console.Core;
using PendriveSyncer.Console.Core.Interfaces;
using PendriveSyncer.Console.Core.Utility;
using PendriveSyncer.FileManagement.Provider;
using PendriveSyncer.FileManagement.Utility;
using PendriveSyncer.WDA.ConsoleApp.Implementation;

namespace PendriveSyncer.WDA.ConsoleApp
{
    class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<StorageDeviceManager>();
            builder.RegisterType<CommandLineParser>();
            builder.RegisterType<StorageDeviceListener>().As<IStorageDeviceListener>();
            builder.RegisterType<LocalStorage>();
            builder.RegisterType<FileSynchoronization>();


            return builder.Build();
        }
        static void Main(string[] args)
        {
            
            CompositionRoot().Resolve<Application>().Run(args);
            System.Console.ReadKey();
        }
    }
}
