using Philomela.Application.Extensions;
using Philomela.Application.Options;
using Philomela.Domain.Extensions;
using Philomela.Infrastructure.Extensions;

namespace Philomela.Api.Extensions
{
    /// <summary>
    /// Расширения подключения зависимостей.
    /// </summary>
    internal static class DiExtensions
    {
        /// <summary>
        /// Подключение зависимостей по слоям.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RegisterLayers(this IServiceCollection services)
        {
            services.AddDomainLayer();
            services.AddApplicationLayer();
            services.AddInfrastructureLayer();

            return services;
        }

        /// <summary>
        /// Подключение настроек.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IServiceCollection"/>.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection RegisterOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtOptions>(configuration.GetSection(SectionNameConstants.Jwt));
            services.Configure<LdapConnection>(configuration.GetSection(SectionNameConstants.LdapConnection));

            return services;
        }

    }
}
