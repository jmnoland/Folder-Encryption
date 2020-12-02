using System.ComponentModel.DataAnnotations;

namespace FolderEncryption.Models
{
    public class Directory
    {
        [Key]
        public int DirectoryId { get; set; }
        public int KeyId { get; set; }
        public string Path { get; set; }
        public EncryptionKey Key { get; set; }
    }
}
