using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace RocheApp.Database.Runner
{
    public class MigratorRunner : IHostedService
    {
        private readonly IMigrator migrator;
        private readonly IConfiguration configuration;

        public MigratorRunner(IMigrator migrator, IConfiguration configuration)
        {
            this.migrator = migrator;
            this.configuration = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken) =>
            Task.Run(() => migrator.Execute(configuration["DatabaseModuleConnectionString"]));

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}