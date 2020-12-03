using Microsoft.EntityFrameworkCore;

namespace FolderEncryption.Models
{
    public class FileEncryptionContext : DbContext
    {
        public DbSet<EncryptionKey> EncryptionKeys { get; set; }
        public DbSet<Folder> Folders { get; set; }

        private static bool _created = false;
        public FileEncryptionContext()
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=fileEncryption.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Folder>()
                .HasOne(p => p.Key)
                .WithMany(b => b.Folders)
                .HasForeignKey(p => p.KeyId)
                .HasPrincipalKey(b => b.KeyId);
        }
    }
}