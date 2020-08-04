using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RocheApp.Domain.Services.Pet;
using RocheApp.Domain.Services.Pet.Interfaces;
using RocheApp.Domain.Services.User;
using RocheApp.Domain.Services.User.Interfaces;

namespace RocheApp.Domain
{
    public static class DependencyInjection
    {
        public static void AddRocheAppDomain(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IPetDeleter, PetDeleter>();
            services.AddScoped<IUserCreator, UserCreator>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserUpdater, UserUpdater>();

            services.AddSingleton(configuration.GetSection(nameof(ApplicationSettings)).Get<ApplicationSettings>());
        }
    }
}