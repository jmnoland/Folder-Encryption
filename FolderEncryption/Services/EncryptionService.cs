using FolderEncryption.Interfaces;
using FolderEncryption.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace FolderEncryption.Services
{
    public class EncryptionService : IEncryptionService
    {
        private IFileEncryptionRepository _fileEncryptionRepository;
        private Dictionary<string, RSACryptoServiceProvider> _publicKeys;
        private Dictionary<string, SymetrickeyDetail> _symetricKeys;
        private RSAEncryptionPadding _padding;
        private readonly RandomNumberGenerator _rng;
        private readonly AppSettings _appSettings;
        private CspParameters _cspParams;
        private AesCryptoServiceProvider _aes;
        public EncryptionService(IFileEncryptionRepository fileEncryptionRepository,
                                 AppSettings appSettings)
        {
            _fileEncryptionRepository = fileEncryptionRepository;
            _padding = RSAEncryptionPadding.OaepSHA1;
            _rng = RandomNumberGenerator.Create();
            _publicKeys = new Dictionary<string, RSACryptoServiceProvider>();
            _symetricKeys = new Dictionary<string, SymetrickeyDetail>();
            _aes = new AesCryptoServiceProvider();
            _appSettings = appSettings;
            _cspParams = new CspParameters
            {
                ProviderName = _appSettings.EncryptionProvider
            };
            if (_appSettings.EncryptionProviderType != null)
            {
                _cspParams.ProviderType = (int)_appSettings.EncryptionProviderType;
            }
            if(_appSettings.EncryptionProviderFlag != null)
            {
                _cspParams.Flags = (CspProviderFlags)_appSettings.EncryptionProviderFlag;
            }
        }

        #region Public methods
        public void CreateNewKey(string containerName, string password, string path)
        {
            var hashPassword = HashPassword(password, _rng, KeyDerivationPrf.HMACSHA256);
            _cspParams.KeyContainerName = containerName;
            if(_appSettings.UsePassword)
            {
                var securePasswordString = new NetworkCredential("", password).SecurePassword;
                _cspParams.KeyPassword = securePasswordString;
            }

            using (var rsa = new RSACryptoServiceProvider(_cspParams))
            {
                CreateSymetricKey();
                var encryptedKey = EncryptKey(_aes.Key, rsa);
                var folder = new Folder
                {
                    Path = path
                };
                _fileEncryptionRepository.AddEncryptionKey(new EncryptionKey
                {
                    CreateDate = DateTime.Now.ToString(),
                    PublicKey = rsa.ToXmlString(false),
                    PublicKeyName = containerName,
                    EncryptedKey = encryptedKey,
                    IV = _aes.IV,
                    Password = hashPassword,
                    Folders = new List<Folder> { folder }
                });
            }
        }

        public void RemoveKey(string containerName)
        {
            _cspParams.KeyContainerName = containerName;
            using (var rsaKey = new RSACryptoServiceProvider(_cspParams))
            {
                rsaKey.PersistKeyInCsp = false;
            }
        }

        public void CreatePublicKeyFromXML(string containerName, string xmlKeyInfo, byte[] encryptedKey, byte[] IV)
        {
            if (_publicKeys.ContainsKey(containerName)) return;
            var publicKey = new RSACryptoServiceProvider();
            publicKey.FromXmlString(xmlKeyInfo);
            _publicKeys.Add(containerName, publicKey);
            _symetricKeys.Add(containerName, new SymetrickeyDetail
                { 
                    EncryptedKey = encryptedKey,
                    KeyIV = IV
                }
            );
        }

        public byte[] EncryptFile(string containerName, byte[] data)
        {
            _cspParams.KeyContainerName = containerName;
            using (var rsaKey = new RSACryptoServiceProvider(_cspParams))
            {
                RSACryptoServiceProvider publicKey = null;
                SymetrickeyDetail aesKey = null;
                _publicKeys.TryGetValue(containerName, out publicKey);
                _symetricKeys.TryGetValue(containerName, out aesKey);
                if (publicKey == null || aesKey == null)
                {
                    rsaKey.PersistKeyInCsp = false;
                    throw new Exception("Encryption failed: Public key not found");
                }

                using (var decryptor = _aes.CreateEncryptor(rsaKey.Decrypt(aesKey.EncryptedKey, _padding), aesKey.KeyIV))
                {
                    byte[] outBlock = decryptor.TransformFinalBlock(data, 0, data.Length);
                    return outBlock;
                }
            }
        }

        public byte[] DecryptFile(string containerName, string hashPassword, string password, byte[] data)
        {
            _cspParams.KeyContainerName = containerName;
            if (_appSettings.UsePassword)
            {
                var securePasswordString = new NetworkCredential("", password).SecurePassword;
                _cspParams.KeyPassword = securePasswordString;
            }
            using (var rsaKey = new RSACryptoServiceProvider(_cspParams))
            {
                RSACryptoServiceProvider publicKey = null;
                SymetrickeyDetail aesKey = null;
                _publicKeys.TryGetValue(containerName, out publicKey);
                _symetricKeys.TryGetValue(containerName, out aesKey);
                if (publicKey == null || aesKey == null)
                {
                    rsaKey.PersistKeyInCsp = false;
                    throw new Exception("Decryption failed: Public key not found");
                }
                if (publicKey.ToXmlString(false) != rsaKey.ToXmlString(false))
                {
                    rsaKey.PersistKeyInCsp = false;
                    throw new Exception("Decryption failed: Doesn't match existing key");
                }

                using(var decryptor = _aes.CreateDecryptor(rsaKey.Decrypt(aesKey.EncryptedKey, _padding), aesKey.KeyIV))
                {
                    byte[] outBlock = decryptor.TransformFinalBlock(data, 0, data.Length);
                    return outBlock;
                }
            }
        }
        #endregion
        #region Private methods

        public void CreateSymetricKey()
        {
            _aes.GenerateKey();
            _aes.GenerateIV();
        }

        private byte[] EncryptKey(byte[] key, RSACryptoServiceProvider rsa)
        {
            return rsa.Encrypt(key, _padding);
        }

        private byte[] DecryptKey(byte[] key, RSACryptoServiceProvider rsa)
        {
            return rsa.Decrypt(key, _padding);
        }

        // https://github.com/aspnet/Identity/blob/c7276ce2f76312ddd7fccad6e399da96b9f6fae1/src/Core/PasswordHasher.cs
        private static string HashPassword(string password, RandomNumberGenerator rng, KeyDerivationPrf prf)
        {
            int iterCount = 10000;
            int saltSize = 128 / 8;
            int numBytesRequested = 256 / 8;

            // Produce a version 3 (see comment above) text hash.
            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

            var outputBytes = new byte[13 + salt.Length + subkey.Length];
            outputBytes[0] = 0x01; // format marker
            WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
            WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
            WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
            return Convert.ToBase64String(outputBytes);
        }
        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));
        }
        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }
        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);
            int iterCount = default(int);
            try
            {
                // Read header information
                KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(decodedHashedPassword, 1);
                iterCount = (int)ReadNetworkByteOrder(decodedHashedPassword, 5);
                int saltLength = (int)ReadNetworkByteOrder(decodedHashedPassword, 9);

                // Read the salt: must be >= 128 bits
                if (saltLength < 128 / 8)
                {
                    return false;
                }
                byte[] salt = new byte[saltLength];
                Buffer.BlockCopy(decodedHashedPassword, 13, salt, 0, salt.Length);

                // Read the subkey (the rest of the payload): must be >= 128 bits
                int subkeyLength = hashedPassword.Length - 13 - salt.Length;
                if (subkeyLength < 128 / 8)
                {
                    return false;
                }
                byte[] expectedSubkey = new byte[subkeyLength];
                Buffer.BlockCopy(decodedHashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                // Hash the incoming password and verify it
                byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);
                return ByteArraysEqual(actualSubkey, expectedSubkey);
            }
            catch
            {
                return false;
            }
        }
        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
        #endregion
    }
}
