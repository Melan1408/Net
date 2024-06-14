using HealthHelper.ViewModels;
using HealthHelper.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;

namespace HealthHelper
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        string projectRootPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\"));
        private readonly IHost _host;
        public IHostBuilder CreateHostBuilder() =>
             Host.CreateDefaultBuilder()
           .ConfigureAppConfiguration((hostContext, config) =>
           {
               config.SetBasePath(projectRootPath);
               config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
               config.AddEnvironmentVariables();
           })
            .ConfigureServices((hostContext, services) => {
                ConfigureServices(hostContext, services);
            });

        public App()
        {
            _host = CreateHostBuilder()
               .Build();
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddScoped<MainView>();
            services.AddScoped<MainViewModel>();
            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(host.Configuration.GetConnectionString("DefaultConnection"));
            });
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();       
            var mainWindow = _host.Services.GetRequiredService<MainView>();
            var mainWindowVM = _host.Services.GetRequiredService<MainViewModel>();
            mainWindow.DataContext = mainWindowVM;
            mainWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
            base.OnExit(e);
        }
    }

}
