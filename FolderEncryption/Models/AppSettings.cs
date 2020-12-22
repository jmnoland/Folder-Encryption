using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderEncryption.Models
{
    public class AppSettings
    {
        public string EncryptionProvider { get; set; }
        public int? EncryptionProviderType { get; set; }
        public int? EncryptionProviderFlag { get; set; }
        public bool UsePassword { get; set; }
    }
}
