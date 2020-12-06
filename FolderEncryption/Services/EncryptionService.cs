using FolderEncryption.Interfaces;
using FolderEncryption.Models;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FolderEncryption.Services
{
    public class EncryptionService : IEncryptionService
    {
        private IFileEncryptionRepository _fileEncryptionRepository;
        private Dictionary<string, RSACryptoServiceProvider> _publicKeys;
        private RSAEncryptionPadding _padding;
        public EncryptionService(IFileEncryptionRepository fileEncryptionRepository)
        {
            _fileEncryptionRepository = fileEncryptionRepository;
            _padding = RSAEncryptionPadding.OaepSHA512;
        }

        public void CreateNewKey(string containerName)
        {
            var param = new CspParameters
            {
                KeyContainerName = containerName
            };
            using(var rsa = new RSACryptoServiceProvider(param))
            {
                _fileEncryptionRepository.AddEncryptionKey(new EncryptionKey
                {
                    CreateDate = DateTime.Now.ToString(),
                    PublicKey = rsa.ToXmlString(false)
                });
            }
        }

        public void CreatePublicKeyFromXML(string containerName, string xmlKeyInfo)
        {
            var publicKey = new RSACryptoServiceProvider();
            publicKey.FromXmlString(xmlKeyInfo);
            _publicKeys.Add(containerName, publicKey);
        }

        public byte[] EncryptFile(string containerName, byte[] data)
        {
            RSACryptoServiceProvider publicKey = null;
            _publicKeys.TryGetValue(containerName, out publicKey);
            if (publicKey == null) throw new Exception("Encryption failed: Public key not found");
            return publicKey.Encrypt(data, _padding);
        }

        public byte[] DecryptFile(string containerName, byte[] data)
        {
            var param = new CspParameters
            {
                KeyContainerName = containerName
            };
            using (var rsaKey = new RSACryptoServiceProvider(param))
            {
                RSACryptoServiceProvider publicKey = null;
                _publicKeys.TryGetValue(containerName, out publicKey);

                if (publicKey == null)
                {
                    rsaKey.PersistKeyInCsp = false;
                    throw new Exception("Decryption failed: Public key not found");
                }
                if (publicKey.ToXmlString(false) != rsaKey.ToXmlString(false))
                {
                    rsaKey.PersistKeyInCsp = false;
                    throw new Exception("Decryption failed: Doesn't match existing key");
                }

                return rsaKey.Decrypt(data, _padding);
            }
        }
    }
}
