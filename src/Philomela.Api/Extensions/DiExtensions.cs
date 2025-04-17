using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Philomela.Api.Db;
using Philomela.Api.Services;
using Philomela.Api.Services.Interfaces;
using Philomela.Application.Options;
using Philomela.Domain.Entities.Authentication;

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
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

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
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            
            return services;
        }
        
        /// <summary>
        ///     Добавление настроек JWT токена.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        /// <exception cref="ArgumentNullException"> Если не найден конфиг токена. </exception>
        public static IServiceCollection AddJwtToken(this IServiceCollection services, IConfiguration configuration)
        {
            JwtOptions jwtOptions =
                configuration.GetSection("Jwt").Get<JwtOptions>() ?? throw new ArgumentNullException(nameof(JwtOptions));

            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                    };
                });
            return services;
        }

    }
}
