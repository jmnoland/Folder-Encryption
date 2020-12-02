using FolderEncryption.Interfaces;
using FolderEncryption.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderEncryption.Services
{
    public class FileEncryptionService : IFileEncryptionService
    {
        private FileEncryptionContext _dbContext;

        public FileEncryptionService(FileEncryptionContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Test()
        {
            Console.WriteLine("Test");
            //EncryptionKey newKey = new EncryptionKey
            //{
            //    CreateDate = DateTime.Now.ToString(),
            //    Directories = new List<Directory>(),
            //    PublicKey = ""
            //};
            //_dbContext.Add(newKey);
            //_dbContext.SaveChanges();
            var test = _dbContext.EncryptionKeys.ToList();

        }

    }
}
