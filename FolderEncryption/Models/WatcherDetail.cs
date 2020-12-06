using System.IO;

namespace FolderEncryption.Models
{
    public class WatcherDetail
    {
        public int KeyId { get; set; }
        public FileSystemWatcher Watcher { get; set; }
    }
}
