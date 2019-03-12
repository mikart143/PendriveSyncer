using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using PendriveSyncer.FileManagment.Enum;

namespace PendriveSyncer.FileManagment.Utility
{
    public class FileSyncHelper
    {
        private string GetFileHash(string filePath, HashTypeEnum hashType)
        {
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists) throw new FileNotFoundException();
            HashAlgorithm hashProvider;
            switch (hashType)
            {
                case HashTypeEnum.MD5:
                    hashProvider = MD5.Create();
                    break;
                case HashTypeEnum.SHA1:
                    hashProvider = SHA1.Create();
                    break;
                case HashTypeEnum.SHA256:
                    hashProvider = SHA256.Create();
                    break;
                default: throw new InvalidEnumArgumentException();
            }

            FileStream fileStream = file.Open(FileMode.Open);

            return hashProvider.ComputeHash(fileStream).ToString();

        }

        private Dictionary<string, string> GetDirectoryHash(string directoryPath, HashTypeEnum hashType)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(directoryPath);
            if (!directoryInfo.Exists) throw new DirectoryNotFoundException();

            List<FileInfo> files = directoryInfo.GetFiles().ToList();
            Dictionary<string, string> filesHashes = new Dictionary<string, string>();
            foreach (var file in files)
            {
                filesHashes.Add(file.Name, GetFileHash(file.FullName, hashType));
            }

            return filesHashes;
        }
    }
}
