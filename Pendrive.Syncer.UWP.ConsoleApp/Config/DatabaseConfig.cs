using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Shapes;
using LiteDB;
using PendriveSyncer.DataAccess;

namespace Pendrive.Syncer.UWP.ConsoleApp.Config
{
    class DatabaseConfig:IDatabaseInit
    {
        private string DBName { get { return "Database.db"; } }

        public override string GetConnectionString()
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var folderPath = localFolder.Path;
            var filePath = System.IO.Path.Combine(folderPath, this.DBName);

            return filePath;
        }
    }
}
