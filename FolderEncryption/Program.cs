using FolderEncryption.Interfaces;
using FolderEncryption.Models;
using FolderEncryption.Repositories;
using FolderEncryption.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderEncryption
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();

            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form1);
            }
        }

        static void ConfigureServices(ServiceCollection services)
        {
            services.AddLogging()
                .AddDbContext<FileEncryptionContext>()
                .AddScoped<Form1>()
                .AddScoped<IFileEncryptionService, FileEncryptionService>()
                .AddScoped<IFileEncryptionRepository, FileEncryptionRepository>()
                .BuildServiceProvider();
        }
    }
}
