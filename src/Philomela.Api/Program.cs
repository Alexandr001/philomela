#pragma warning disable CA1506
using Microsoft.EntityFrameworkCore;
using Philomela.Api.Extensions;
using Philomela.Api.Middlewares;
using Philomela.Api.Swagger;
using Philomela.Application.Extensions;
using Philomela.Application.Options;
using Philomela.Infrastructure;
using Philomela.Infrastructure.Extensions;
using Serilog;

namespace Philomela.Api
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.RegisterOptions(builder.Configuration);
            builder.Services.AddDbContext<BaseDbContext>(o =>
                o.UseNpgsql(builder.Configuration.GetConnectionString(SectionNameConstants.PgConnection)));
            
            builder.Services.AddSerilog(options =>
                options
                    .ReadFrom.Configuration(builder.Configuration)
                    .WriteTo.Console());

            // Add services to the container.
            builder.Services.AddCors();
            builder.Services.AddControllers();
            builder.Services.AddApiVersioning();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddJwtToken(builder.Configuration);
            builder.Services.RegisterLayers();

            builder.Services.AddSwaggerWithVersioning();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerWithVersioning();
            }

            await app.UseMigrate();

            app.UseRouting();

            app.UseCors(cpb => cpb.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            await app.RunAsync();
        }
    }
}
