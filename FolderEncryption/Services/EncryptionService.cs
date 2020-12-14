using FolderEncryption.Interfaces;
using FolderEncryption.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;

namespace FolderEncryption.Services
{
    public class EncryptionService : IEncryptionService
    {
        private IFileEncryptionRepository _fileEncryptionRepository;
        private Dictionary<string, RSACryptoServiceProvider> _publicKeys;
        private RSAEncryptionPadding _padding;
        private readonly RandomNumberGenerator _rng;
        public EncryptionService(IFileEncryptionRepository fileEncryptionRepository)
        {
            _fileEncryptionRepository = fileEncryptionRepository;
            _padding = RSAEncryptionPadding.OaepSHA512;
            _rng = RandomNumberGenerator.Create();
        }

        #region Public methods
        public void CreateNewKey(string containerName, string password)
        {
            var hashPassword = HashPassword(password, _rng, KeyDerivationPrf.HMACSHA256);
            var securePasswordString = new NetworkCredential("", password).SecurePassword;
            var param = new CspParameters
            {
                KeyContainerName = containerName,
                KeyPassword = securePasswordString,
                ProviderType = 1,
                ProviderName = "Microsoft Base Smart Card Crypto Provider"
                //Flags = CspProviderFlags.UseNonExportableKey | CspProviderFlags.UseUserProtectedKey
            };
            using (var rsa = new RSACryptoServiceProvider(param))
            {
                _fileEncryptionRepository.AddEncryptionKey(new EncryptionKey
                {
                    CreateDate = DateTime.Now.ToString(),
                    PublicKey = rsa.ToXmlString(false),
                    PublicKeyName = containerName,
                    Password = hashPassword
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

        public byte[] DecryptFile(string containerName, string hashPassword, string password, byte[] data)
        {
            var isValid = VerifyHashedPassword(hashPassword, password);
            if (!isValid) throw new Exception("Decryption failed: Password doesn't match");

            var securePasswordString = new NetworkCredential("", password).SecurePassword;
            var param = new CspParameters
            {
                KeyContainerName = containerName,
                KeyPassword = securePasswordString
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
        #endregion
        #region Private methods

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
