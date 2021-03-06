﻿using FolderEncryption.Interfaces;
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

        public List<Folder> GetDirectories()
        {
            return _dbContext.Folders.ToList();
        }
        public List<Folder> GetDirectoriesFromKey(EncryptionKey key)
        {
            return _dbContext.Folders.Where(w => w.KeyId == key.KeyId).ToList();
        }
        public List<EncryptionKey> GetEncryptionKeys()
        {
            return _dbContext.EncryptionKeys
                .Join(_dbContext.Folders,
                    e => e.KeyId,
                    f => f.KeyId,
                    (e, f) => new EncryptionKey
                    {
                        KeyId = e.KeyId,
                        Password = e.Password,
                        PublicKey = e.PublicKey,
                        PublicKeyName = e.PublicKeyName,
                        EncryptedKey = e.EncryptedKey,
                        IV = e.IV,
                        CreateDate = e.CreateDate,
                        Folders = new List<Folder> { f },
                    })
                .ToList();
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
        public async void AddDirectory(Folder directory)
        {
            _dbContext.Folders.Add(directory);
            await _dbContext.SaveChangesAsync();
        }
        public async void RemoveDirectory(Folder directory)
        {
            _dbContext.Folders.Remove(directory);
            await _dbContext.SaveChangesAsync();
        }
    }
}
