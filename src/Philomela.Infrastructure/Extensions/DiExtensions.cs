using Microsoft.Extensions.DependencyInjection;
using Philomela.Application.Abstractions;
using Philomela.Domain.Entities.Authentication;
using Philomela.Domain.Entities.Philomela;
using Philomela.Domain.Entities.User;
using Philomela.Infrastructure.Database.Repositories;
using Philomela.Infrastructure.Providers;

namespace Philomela.Infrastructure.Extensions
{
    /// <summary>
    /// Расширения подключения зависимостей.
    /// </summary>
    public static class DiExtensions
    {
        /// <summary>
        /// Подключение зависимостей Infrastructure.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddRepositories();
            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ILdapProvider, LdapProvider>();
            
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            services.AddScoped<IPhilomelaRepository, PhilomelaRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
