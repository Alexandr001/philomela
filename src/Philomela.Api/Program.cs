#pragma warning disable CA1506

using Microsoft.EntityFrameworkCore;
using Philomela.Api.Db;
using Philomela.Api.Extensions;
using Philomela.Api.Middlewares;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterOptions(builder.Configuration);
builder.Services.AddDbContext<BaseDbContext>(o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("PgSql")));

builder.Services.AddLogging();

// Add services to the container.
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddJwtToken(builder.Configuration);
builder.Services.RegisterLayers();

builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseHsts();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

await app.UseMigrate();

app.UseRouting();

app.UseCors(cpb => cpb.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
