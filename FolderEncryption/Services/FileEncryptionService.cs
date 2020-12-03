using FolderEncryption.Interfaces;
using FolderEncryption.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderEncryption.Services
{
    public class FileEncryptionService : IFileEncryptionService
    {
        private IFileEncryptionRepository _fileEncryptionRepository;
        public FileEncryptionService(IFileEncryptionRepository fileEncryptionRepository)
        {
            _fileEncryptionRepository = fileEncryptionRepository;
        }

        public void StartThreads()
        {
            var encryptionKeys = _fileEncryptionRepository.GetEncryptionKeys();
            foreach (var key in encryptionKeys)
            {
                foreach(var dir in key.Folders)
                {
                    BeginWatching(dir, key);
                }
            }
        }
        private static void BeginWatching(Folder directory, EncryptionKey key)
        {
            using (FileSystemWatcher watcher = new FileSystemWatcher())
            {
                watcher.Path = directory.Path;

                watcher.NotifyFilter = NotifyFilters.LastAccess
                                     | NotifyFilters.LastWrite
                                     | NotifyFilters.FileName
                                     | NotifyFilters.DirectoryName;

                watcher.Changed += (source, e) => OnChanged(source, e, key);
                watcher.Created += (source, e) => OnChanged(source, e, key);
                watcher.Deleted += (source, e) => OnChanged(source, e, key);
                watcher.Renamed += OnRenamed;

                watcher.EnableRaisingEvents = true;
            }
        }

        private static void OnChanged(object source, FileSystemEventArgs e, EncryptionKey key)
        {

        }

        private static void OnRenamed(object source, RenamedEventArgs e) =>
            Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");
    }
}
