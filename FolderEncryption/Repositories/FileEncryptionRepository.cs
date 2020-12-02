using FolderEncryption.Interfaces;
using FolderEncryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderEncryption.Repositories
{
    public class FileEncryptionRepository : IFileEncryptionRepository
    {
        private FileEncryptionContext _dbContext;

        public FileEncryptionRepository(FileEncryptionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Directory> GetDirectories()
        {
            return _dbContext.Directories.ToList();
        }
        public List<Directory> GetDirectoriesFromKey(EncryptionKey key)
        {
            return _dbContext.Directories.Where(w => w.KeyId == key.KeyId).ToList();
        }
        public List<EncryptionKey> GetEncryptionKeys()
        {
            return _dbContext.EncryptionKeys.ToList();
        }
        public async void AddEncryptionKey(EncryptionKey key)
        {
            _dbContext.EncryptionKeys.Add(key);
            await _dbContext.SaveChangesAsync();
        }
        public async void RemoveEncryptionKey(EncryptionKey key)
        {
            _dbContext.EncryptionKeys.Remove(key);
            await _dbContext.SaveChangesAsync();
        }
        public async void AddDirectory(Directory directory)
        {
            _dbContext.Directories.Add(directory);
            await _dbContext.SaveChangesAsync();
        }
        public async void RemoveDirectory(Directory directory)
        {
            _dbContext.Directories.Remove(directory);
            await _dbContext.SaveChangesAsync();
        }
    }
}
