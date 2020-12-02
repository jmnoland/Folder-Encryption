using FolderEncryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderEncryption.Interfaces
{
    public interface IFileEncryptionRepository
    {
        List<Directory> GetDirectories();
        List<Directory> GetDirectoriesFromKey(EncryptionKey key);
        List<EncryptionKey> GetEncryptionKeys();
        void AddEncryptionKey(EncryptionKey key);
        void RemoveEncryptionKey(EncryptionKey key);
        void AddDirectory(Directory directory);
        void RemoveDirectory(Directory directory);
    }
}
