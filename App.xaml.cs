using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Test2.View;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Test2.Data;
using Test2.Services;
using Test2.Services.Abstract;
using Test2.ViewModel;

using System.Windows;

namespace Test2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration Config { get; private set; }
        private readonly IHost _host;
        public App()
        {
            Config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();
            _host = Host.CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddScoped<Test2VM>();
                services.AddDbContext<Test2Context>(opt => opt.UseSqlServer(Config.GetConnectionString("DataBase")));
                services.AddScoped<ITestsService, TestsService>();
                services.AddScoped<IParametersService,ParametersService>();
            })
            .Build();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();
            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }
        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
