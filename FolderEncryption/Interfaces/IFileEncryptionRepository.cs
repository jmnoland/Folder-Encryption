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
        List<Folder> GetDirectories();
        List<Folder> GetDirectoriesFromKey(EncryptionKey key);
        List<EncryptionKey> GetEncryptionKeys();
        void AddEncryptionKey(EncryptionKey key);
        void RemoveEncryptionKey(EncryptionKey key);
        void AddDirectory(Folder directory);
        void RemoveDirectory(Folder directory);
    }
}
