using System;
using Autofac;
using Pendrive.Syncer.UWP.ConsoleApp.Config;
using Pendrive.Syncer.UWP.ConsoleApp.Implementation;
using Pendrive.Syncer.UWP.ConsoleApp.Utility;
using PendriveSyncer.Console.Core;
using PendriveSyncer.Console.Core.Interfaces;
using PendriveSyncer.Console.Core.Utility;
using PendriveSyncer.DataAccess;
using PendriveSyncer.DataAccess.Services;
using Usb.Net.UWP;


// This example code shows how you could implement the required main function for a 
// Console UWP Application. You can replace all the code inside Main with your own custom code.

// You should also change the Alias value in the AppExecutionAlias Extension in the 
// Package.appxmanifest to a value that you define. To edit this file manually, right-click
// it in Solution Explorer and select View Code, or open it with the XML Editor.

namespace Pendrive.Syncer.UWP.ConsoleApp
{
    static class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<DatabaseService>();
            builder.RegisterType<DatabaseAccess>();
            builder.RegisterType<DatabaseConfig>().As<IDatabaseInit>();
            builder.RegisterType<StorageDevice>().As<IStorageDevice>();
            builder.RegisterType<CommandLineParser>();
            builder.RegisterType<StorageDeviceListener>().As<IStorageDeviceListener>();
            builder.RegisterType<StorageDeviceManager>();
            

            return builder.Build();
        }
        static void Main(string[] args)
        {

            CompositionRoot().Resolve<Application>().Run(args);

            
            Console.WriteLine("Press a key to continue: ");
            Console.ReadLine();
        }
    }
}
