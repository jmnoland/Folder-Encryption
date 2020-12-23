using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderEncryption.Interfaces
{
    public interface IEncryptionService
    {
        void CreateNewKey(string containerName, string password, string path);
        void RemoveKey(string containerName);
        void CreatePublicKeyFromXML(string containerName, string xmlKeyInfo, byte[] encryptedKey, byte[] IV);
        byte[] EncryptFile(string containerName, byte[] data);
        byte[] DecryptFile(string containerName, string hashPassword, string password, byte[] data);
    }
}
