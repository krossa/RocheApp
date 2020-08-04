using Microsoft.Extensions.DependencyInjection;

namespace RocheApp.Database.DbUp
{
    public static class DependencyInjection
    {
        public static void AddRocheAppDatabaseDbUp(this IServiceCollection services)
        {
            services.AddScoped<IMigrator, DbUpSqlServerMigrator>();
        }

    }
}
