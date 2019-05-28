using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PendriveSyncer.Console.Core.Model;
using PendriveSyncer.DataAccess.Services;

namespace PendriveSyncer.FileManagement.Utility
{
    public class FileUtility
    {
        private readonly DatabaseService _databaseService;
        public FileUtility(DatabaseService databaseService)
        {

            _databaseService = databaseService;
        }


        public List<string> GetFilteredList(List<string> fileList, StorageDevice device, string storagePath)
        {
            var ignoredItems = _databaseService.GetIgnoredItems(device.SerialNumber);
            foreach (var ignored in ignoredItems)
            {
                fileList.RemoveAll(x => x == (storagePath + ignored));
            }

            return fileList;
        }

        public List<string> GetFileList(DirectoryInfo source)
        {
            var bagOfPath = new ConcurrentBag<string>();
            if(!source.Exists) return new List<string>();

            try
            {
                var files = source.GetFiles();
                Parallel.ForEach(files, x =>
                {
                    bagOfPath.Add(x.FullName.ToString());
                });
            }
            catch (Exception e)
            {
                
            }

            try
            {
                var directories = source.GetDirectories();
                Parallel.ForEach(directories, x =>
                {
                    foreach (var file in GetFileList(x))
                    {
                        bagOfPath.Add(file);
                    }
                });
            }
            catch (Exception e)
            {
                
            }

            return bagOfPath.ToList();
        }

    }
}