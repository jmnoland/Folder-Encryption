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
    public class FileWatcherService : IFileWatcherService
    {
        private IFileEncryptionRepository _fileEncryptionRepository;
        private Dictionary<int, WatcherDetail> _watchers;
        public FileWatcherService(IFileEncryptionRepository fileEncryptionRepository)
        {
            _fileEncryptionRepository = fileEncryptionRepository;
            StartWatchers();
        }

        public void StartWatchers()
        {
            var encryptionKeys = _fileEncryptionRepository.GetEncryptionKeys();
            foreach (var key in encryptionKeys)
            {
                foreach (var dir in key.Folders)
                {
                    WatcherDetail temp = null;
                    _watchers.TryGetValue(dir.DirectoryId, out temp);

                    // Check if key has been changed
                    if (temp.KeyId == key.KeyId) continue;
                    temp.Watcher.Dispose();

                    _watchers.Add(dir.DirectoryId, BeginWatching(dir, key));
                }
            }
        }
        private static WatcherDetail BeginWatching(Folder directory, EncryptionKey key)
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

                return new WatcherDetail
                {
                    KeyId = key.KeyId,
                    Watcher = watcher
                };
            }
        }

        private static void OnChanged(object source, FileSystemEventArgs e, EncryptionKey key)
        {
            Console.WriteLine($"File: {e.FullPath}");
        }

        private static void OnRenamed(object source, RenamedEventArgs e) =>
            Console.WriteLine($"File: {e.OldFullPath} renamed to {e.FullPath}");
    }
}
