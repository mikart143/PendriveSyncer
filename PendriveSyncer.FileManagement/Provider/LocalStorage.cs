using System.IO;
using PendriveSyncer.Console.Core.Interfaces;

namespace PendriveSyncer.FileManagement.Provider
{
    public class LocalStorage:IStorageProvider
    {
        public void Brutal(string destination, string source)
        {
            DirectoryInfo destinationDirectory = new DirectoryInfo(destination);
            DirectoryInfo sourceDirectory = new DirectoryInfo(source);

            if (destinationDirectory.Exists)
            {
                destinationDirectory.Delete();
                destinationDirectory.Create();
            }
            else
            {
                destinationDirectory.Create();
            }
            
            foreach (var file in sourceDirectory.GetFiles())
            {
                file.CopyTo(destinationDirectory.FullName +"\\"+ file.Name,true);
            }
            
        }
    }
}