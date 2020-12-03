using FolderEncryption.Interfaces;
using FolderEncryption.Models;
using FolderEncryption.Repositories;
using FolderEncryption.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderEncryption
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<FileEncryptionContext>();
            services.AddSingleton<IFileWatcherService, FileWatcherService>();
            services.AddScoped<IEncryptionService, EncryptionService>();
            services.AddScoped<IFileEncryptionRepository, FileEncryptionRepository>();
        }
    }
}
