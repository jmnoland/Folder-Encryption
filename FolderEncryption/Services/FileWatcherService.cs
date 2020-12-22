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
        private IEncryptionService _encryptionService;
        private Dictionary<int, WatcherDetail> _watchers = new Dictionary<int, WatcherDetail>();
        public FileWatcherService(IFileEncryptionRepository fileEncryptionRepository,
                                  IEncryptionService encryptionService)
        {
            _fileEncryptionRepository = fileEncryptionRepository;
            _encryptionService = encryptionService;
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
                    if (temp != null)
                    {
                        if (temp.KeyId == key.KeyId) continue;
                        temp.Watcher.Dispose();
                    }

                    _watchers.Add(dir.DirectoryId, BeginWatching(dir, key, _encryptionService));
                }
                _encryptionService.CreatePublicKeyFromXML(key.PublicKeyName, key.PublicKey);
            }
        }
        private static WatcherDetail BeginWatching(Folder directory, EncryptionKey key, IEncryptionService encryptionService)
        {
            FileSystemWatcher watcher = new FileSystemWatcher();
            {
                watcher.Path = directory.Path;

                watcher.NotifyFilter = NotifyFilters.CreationTime |
                                       NotifyFilters.LastWrite;

                watcher.Created += (source, e) => OnChanged(source, e, key, encryptionService);

                watcher.EnableRaisingEvents = true;

                return new WatcherDetail
                {
                    KeyId = key.KeyId,
                    Watcher = watcher
                };
            }
        }

        private static void OnChanged(object source, FileSystemEventArgs e, EncryptionKey key, IEncryptionService encryptionService)
        {
            var data = File.ReadAllBytes(e.FullPath);
            encryptionService.EncryptFile(key.PublicKeyName, data);
            Console.WriteLine($"File: {e.FullPath}");
        }
    }
}
