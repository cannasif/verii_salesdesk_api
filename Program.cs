using salesdesk_api.Shared.Host.WebApi.Extensions;
using salesdesk_api.Shared.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var designTimeDbContextOnly = builder.Configuration.GetValue<bool>("SalesdeskRuntime:DesignTimeDbContextOnly");

if (designTimeDbContextOnly)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("DefaultConnection is not configured.");

    builder.Services.AddDbContext<SalesDeskDbContext>(options =>
    {
        options.UseSqlServer(connectionString, sqlOptions =>
        {
            sqlOptions.CommandTimeout(60);
        });
    });

    builder.Build();
    return;
}

var configuredCorsOrigins = builder.Configuration
    .GetSection("Cors:AllowedOrigins")
    .Get<string[]>()
    ?? Array.Empty<string>();

var configuredCorsOriginPatterns = builder.Configuration
    .GetSection("Cors:AllowedOriginPatterns")
    .Get<string[]>()
    ?? Array.Empty<string>();

configuredCorsOrigins = CorsOriginMatcher
    .NormalizeAllowedOrigins(configuredCorsOrigins.Concat(configuredCorsOriginPatterns));

builder.Services.AddSalesdeskApiWebApi(builder.Configuration, builder.Environment, configuredCorsOrigins);

var app = builder.Build();

app.UseSalesdeskApiWebApi(configuredCorsOrigins);

app.Run();

public partial class Program { }
