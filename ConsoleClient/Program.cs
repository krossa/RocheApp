using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RocheApp.DataAccess.Dapper;
using RocheApp.Domain;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleClient
{
    class Program
    {
        private static ServiceProvider _serviceProvider;

        static async Task Main(string[] args)
        {
            RegisterServices();
            using var scope = _serviceProvider.CreateScope();
            var app = scope.ServiceProvider.GetService<Application>();
            await app.Run();
            DisposeServices();
        }

        private static void RegisterServices()
        {
            var config = LoadConfiguration();

            var services = new ServiceCollection();
            services.AddTransient<Application>();
            services.AddRocheAppDomain(config);
            services.AddDataAccessDapper(config);
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices() => _serviceProvider?.Dispose();

        private static IConfiguration LoadConfiguration() =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
    }
}