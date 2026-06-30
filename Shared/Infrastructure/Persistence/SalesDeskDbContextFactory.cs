using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace salesdesk_api.Shared.Infrastructure.Persistence
{
    public class SalesDeskDbContextFactory : IDesignTimeDbContextFactory<SalesDeskDbContext>
    {
        public SalesDeskDbContext CreateDbContext(string[] args)
        {
            var basePath = Directory.GetCurrentDirectory();

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
                ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("General.DefaultConnectionStringNotConfigured");

            var optionsBuilder = new DbContextOptionsBuilder<SalesDeskDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SalesDeskDbContext(optionsBuilder.Options);
        }
    }
}
