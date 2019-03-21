using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PendriveSyncer.FileManagement.Utility
{
    public class FileSynchoronization
    {
        private List<string> _sourceFilesPath = new List<string>();
        public FileSynchoronization()
        {
//            if(!source.Exists || !destination.Exists) throw new DirectoryNotFoundException();

        }

        public void Run()
        {
            CreateSourceFileList(new DirectoryInfo("E:\\"));
            foreach (var x in pathBag.ToList())
            {
                System.Console.WriteLine(x);
            }
        }

        

        private ConcurrentBag<string> pathBag = new ConcurrentBag<string>();
        private void CreateSourceFileList(DirectoryInfo source)
        {
            

            try
            {
                var files = source.GetFiles();
                Parallel.ForEach(files, x =>
                {
                    pathBag.Add(x.FullName.ToString());
                });
            }
            catch (Exception e)
            {
                
            }

            try
            {
                var directories = source.GetDirectories();
                if(directories != null)
                    Parallel.ForEach(directories, x => { CreateSourceFileList(x); });
            }
            catch (Exception e)
            {
                
            }

            
        }

    }
}