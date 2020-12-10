using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderEncryption.Interfaces
{
    public interface IEncryptionService
    {
        void CreateNewKey(string containerName, string password);
        void CreatePublicKeyFromXML(string containerName, string xmlKeyInfo);
        byte[] EncryptFile(string containerName, byte[] data);
        byte[] DecryptFile(string containerName, string hashPassword, string password, byte[] data);
    }
}
