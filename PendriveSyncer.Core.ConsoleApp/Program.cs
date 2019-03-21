using System;
using System.IO;
using System.Runtime.InteropServices;
using Autofac;
using PendriveSyncer.Console.Core;

namespace PendriveSyncer.Core.ConsoleApp
{
    class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();



            return builder.Build();
        }
        static void Main(string[] args)
        {
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                System.Console.WriteLine("Not ssupported currently");
                return;
            }
            CompositionRoot().Resolve<Application>().Run(args);
        }
    }
}
