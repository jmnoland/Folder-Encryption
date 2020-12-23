
namespace FolderEncryption.Models
{
    public class SymetrickeyDetail
    {
        public byte[] EncryptedKey { get; set; }
        public byte[] KeyIV { get; set; }
    }
}
