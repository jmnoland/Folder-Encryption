using FolderEncryption.Interfaces;
using FolderEncryption.Models;
using FolderEncryption.Repositories;
using FolderEncryption.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
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

            AppSettings appSettings = new AppSettings();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var configuration = builder.Build();
            ConfigurationBinder.Bind(configuration.GetSection("AppSettings"), appSettings);

            ConfigureServices(services, appSettings);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<Form1>();
                Application.Run(form1);
            }
        }

        static void ConfigureServices(ServiceCollection services, AppSettings appSettings)
        {
            services.AddLogging()
                .AddDbContext<FileEncryptionContext>()
                .AddScoped<Form1>()
                .AddSingleton(appSettings)
                .AddSingleton<IFileWatcherService, FileWatcherService>()
                .AddSingleton<IEncryptionService, EncryptionService>()
                .AddScoped<IFileEncryptionRepository, FileEncryptionRepository>()
                .BuildServiceProvider();
        }
    }
}
