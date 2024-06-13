using Microsoft.Extensions.DependencyInjection;

namespace Philomela.Domain.Extensions
{
    /// <summary>
    /// Расширения подключения зависимостей.
    /// </summary>
    public static class DiExtensions
    {
        /// <summary>
        /// Подключение зависимостей Domain.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddDomainLayer(this IServiceCollection services)
        {
            return services;
        }
    }
}
