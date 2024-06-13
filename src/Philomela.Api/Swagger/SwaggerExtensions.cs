using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Microsoft.OpenApi.Models;
using Philomela.Application.ActionsFilters.ApiKey;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Philomela.Api.Swagger
{
    /// <summary>
    /// Расширения для Swagger.
    /// </summary>
    internal static class SwaggerExtensions
    {
        private static readonly string[] _xmlDocFilename = new string[]
        {
            "api.xml", "application.xml", "domain.xml", "infrastructure.xml",
        };

        /// <summary>
        ///     Подключение Swagger.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns><see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddSwaggerWithVersioning(this IServiceCollection services)
        {
            services
                .AddSwaggerGen(c =>
                {
                    c.IncludeXmlComments();
                    c.AddAuthorizationJwt();
                    c.AddAuthorizationApiKey();
                })
                .ConfigureOptions<ConfigureSwaggerOptions>()
                .AddEndpointsApiExplorer()
                .AddApiVersioning(o =>
                {
                    o.ReportApiVersions = true;
                    o.AssumeDefaultVersionWhenUnspecified = true;
                    o.DefaultApiVersion = new ApiVersion(1, 0);
                })
                .AddApiExplorer(setup =>
                {
                    setup.GroupNameFormat = "'v'VVV";
                    setup.SubstituteApiVersionInUrl = true;
                });

            return services;
        }

        /// <summary>
        ///     Подключение Swagger.
        /// </summary>
        /// <param name="builder"><see cref="IApplicationBuilder"/>.</param>
        /// <returns><see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseSwaggerWithVersioning(this IApplicationBuilder builder)
        {
            var apiVersionDescriptionProvider =
                builder.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            builder.UseSwagger();
            builder.UseSwaggerUI(options =>
            {
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }
            });

            return builder;
        }

        /// <summary>
        ///     Подключить комментарии XML документации.
        /// </summary>
        /// <param name="swaggerGenOptions"><see cref="SwaggerGenOptions"/>.</param>
        private static void IncludeXmlComments(this SwaggerGenOptions swaggerGenOptions)
        {
            foreach (string xmlDocFile in _xmlDocFilename)
            {
                string filePath = Path.Combine(AppContext.BaseDirectory, xmlDocFile);
                if (File.Exists(filePath))
                {
                    swaggerGenOptions.IncludeXmlComments(filePath, true);
                    swaggerGenOptions.EnableAnnotations();
                }
            }
        }

        /// <summary>
        ///     Добавить авторизацию по токену. 
        /// </summary>
        /// <returns></returns>
        private static SwaggerGenOptions AddAuthorizationJwt(this SwaggerGenOptions opts)
        {
            const string BEARER_NAME = "Bearer";
            opts.AddSecurityDefinition(BEARER_NAME,
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                });
            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = BEARER_NAME },
                    },
                    new string[] { }
                }
            });
            return opts;
        }

        /// <summary>
        ///     Добавить авторизацию по Api-Key
        /// </summary>
        /// <returns></returns>
        private static SwaggerGenOptions AddAuthorizationApiKey(this SwaggerGenOptions opts)
        {
            const string API_KEY_NAME = "APIkey";

            opts.AddSecurityDefinition(API_KEY_NAME,
                new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter API-KEY",
                    Name = ApiKeyAuthorizationFilter.XApiKey,
                    Type = SecuritySchemeType.ApiKey,
                });
            opts.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = API_KEY_NAME, },
                    },
                    new string[] { }
                }
            });
            return opts;
        }
    }
}
