using System.Text;
using FluentValidation.AspNetCore;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Philomela.Application.ActionsFilters.ApiKey;
using Philomela.Application.Common.Behaviors;
using Philomela.Application.Common.Services;
using Philomela.Application.Common.Services.Interfaces;
using Philomela.Application.Options;

namespace Philomela.Application.Extensions
{
    /// <summary>
    ///     Расширения подключения зависимостей Application.
    /// </summary>
    public static class DiExtensions
    {
        /// <summary>
        ///     Подключение зависимостей.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddMapster();
            services
                .AddMediatR(options => 
                {
                    options.RegisterServicesFromAssemblyContaining(typeof(DiExtensions));
                });

            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddSingleton<ApiKeyAuthorizationFilter>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IPhilomelaService, PhilomelaService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<ICalculationService, CalculationService>();
            
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
                configuration.GetSection(SectionNameConstants.Jwt).Get<JwtOptions>() ?? throw new ArgumentNullException(nameof(JwtOptions));

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
