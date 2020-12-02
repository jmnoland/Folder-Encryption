using Microsoft.EntityFrameworkCore;

namespace FolderEncryption.Models
{
    public class FileEncryptionContext : DbContext
    {
        public DbSet<EncryptionKey> EncryptionKeys { get; set; }
        public DbSet<Directory> Directories { get; set; }

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
            modelBuilder.Entity<Directory>()
                .HasOne(p => p.Key)
                .WithMany(b => b.Directories)
                .HasForeignKey(p => p.KeyId)
                .HasPrincipalKey(b => b.KeyId);
        }
    }
}