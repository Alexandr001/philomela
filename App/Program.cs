#pragma warning disable CA1506

using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using App.Db;
using App.Extensions;
using App.Middlewares;
using Microsoft.AspNetCore.Server.Kestrel.Https;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var httpsConnectionAdapterOptions = new HttpsConnectionAdapterOptions
{
    SslProtocols = System.Security.Authentication.SslProtocols.Tls12,
    ClientCertificateMode = ClientCertificateMode.AllowCertificate,
    ServerCertificate = new X509Certificate2(
        builder.Configuration["Certificate:Path"] ?? "./https/certificate.pfx", 
        builder.Configuration["Certificate:Password"]
    )

};
builder.WebHost.ConfigureKestrel(options =>
    options.ConfigureEndpointDefaults(listenOptions =>
        listenOptions.UseHttps(httpsConnectionAdapterOptions)));

builder.Services.RegisterOptions(builder.Configuration);
builder.Services.AddDbContext<BaseDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("PgSql")));

builder.Services.AddLogging();

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtToken(builder.Configuration);
builder.Services.RegisterLayers();

builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();
await app.UseMigrate();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.MapControllers();

app.UseMiddleware<TokenMiddleware>();

app.UseCors(cpb => cpb.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}");

await app.RunAsync();
