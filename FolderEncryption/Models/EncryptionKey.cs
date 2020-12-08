using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FolderEncryption.Models
{
    public class EncryptionKey
    {
        [Key]
        public int KeyId { get; set; }
        public string PublicKey { get; set; }
        public string PublicKeyName { get; set; }
        public string Password { get; set; }
        public string CreateDate { get; set; }
        public List<Folder> Folders { get; set; }
    }
}
