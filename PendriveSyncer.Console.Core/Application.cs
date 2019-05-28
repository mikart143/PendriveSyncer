using CommandLine;
using PendriveSyncer.Console.Core.Model;
using PendriveSyncer.Console.Core.Utility;
using PendriveSyncer.FileManagement.Provider;
using PendriveSyncer.FileManagement.Utility;

namespace PendriveSyncer.Console.Core
{
    public class Application
    {
        private readonly CommandLineParser _commandLineParser;
//        private readonly StorageDeviceManager _storageDeviceManager;
        private readonly LocalStorage _localStorage;
        public Application(CommandLineParser commandLineParser,  LocalStorage localStorage, FileUtility sync)
        {
            _commandLineParser = commandLineParser;
//            _storageDeviceManager = storageDeviceManager;
//            _localStorage = localStorage;
sync.Run();

        }
        public void Run(string [] args)
        {
//           _localStorage.Brutal("D://Kopia","F://");
            var result = _commandLineParser.ParseArguments(args);
            result.WithParsed<CommandOptions>(x =>
            {
                if (x.Silent)
                {
                    
                }
                else
                {
                    
                }
            });
        }
    }
}