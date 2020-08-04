using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RocheApp.DataAccess.Dapper.Repositories;
using RocheApp.Domain.Repositories;

namespace RocheApp.DataAccess.Dapper
{
    public static class DependencyInjection
    {
        public static void AddDataAccessDapper(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPetRepository, PetRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            
            services.AddSingleton(configuration.GetSection(nameof(DataAccessSettings)).Get<DataAccessSettings>());
        }

    }
}
