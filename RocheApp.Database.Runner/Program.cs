using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RocheApp.Database.DbUp;

namespace RocheApp.Database.Runner
{
    class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddRocheAppDatabaseDbUp();
                    services.AddHostedService<MigratorRunner>();
                })
                .RunConsoleAsync();
        }
    }
}
