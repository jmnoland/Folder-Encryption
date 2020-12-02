using FolderEncryption.Interfaces;
using FolderEncryption.Models;
using System;
using System.Collections.Generic;
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
                
            }
        }
        private static void BeginWatching(Directory directory)
        {
            using (System.IO.FileSystemWatcher watcher = new System.IO.FileSystemWatcher())
            {
                watcher.Path = directory.Path;

                watcher.NotifyFilter = System.IO.NotifyFilters.LastAccess
                                     | System.IO.NotifyFilters.LastWrite
                                     | System.IO.NotifyFilters.FileName
                                     | System.IO.NotifyFilters.DirectoryName;

                watcher.Changed += OnChanged;
                watcher.Created += OnChanged;
                watcher.Deleted += OnChanged;
                watcher.Renamed += OnRenamed;

                watcher.EnableRaisingEvents = true;
            }
        }

        private static void OnChanged(object source, System.IO.FileSystemEventArgs e) =>
            Console.WriteLine($"File: {e.FullPath} {e.ChangeType}");

        private static void OnRenamed(object source, System.IO.RenamedEventArgs e) =>
            Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");
    }
}
