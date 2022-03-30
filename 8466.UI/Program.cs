using _8466.Application.Interfaces;
using _8466.Infrastructure.Data;
using _8466.Infrastructure.Migrations;
using _8466.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.Entity;
using System.Windows.Forms;

namespace ConcProg_CW1_8466
{
    internal static class Program
    {
        public static IServiceProvider ServiceProvider { get; set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
            ConfigureServices();
            Application.Run(new MainWindow());
        }

        static void ConfigureServices()
        {
            var services = new ServiceCollection();
            services.AddScoped((_) => new DataContext());
            services.AddScoped<ISwipeService, SwipeService>();
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
