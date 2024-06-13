using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Philomela.Infrastructure.Extensions
{
    /// <summary>
    ///     Расширение миграций
    /// </summary>
    public static class MigrateExtension
    {
        /// <summary>
        ///     Метод добавления миграций
        /// </summary>
        /// <param name="builder"> <see cref="IApplicationBuilder"/>. </param>
        public static async Task UseMigrate(this IApplicationBuilder builder)
        {
            using IServiceScope scope = builder.ApplicationServices.CreateScope();
        
            BaseDbContext context = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
            await context.Database.MigrateAsync();
        }
    }
}
