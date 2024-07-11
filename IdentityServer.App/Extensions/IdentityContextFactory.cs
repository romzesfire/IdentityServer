using IdentityServer.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityServer.App.Extensions;

public class IdentityContextFactory : IDesignTimeDbContextFactory<IdentityServerDbContext>
{
    public IdentityServerDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityServerDbContext>();

        var builder = new ConfigurationBuilder();
        builder.SetBasePath(Directory.GetCurrentDirectory());
        builder.AddJsonFile("appsettings.json");
        var config = builder.Build();

        var connectionString = config.GetConnectionString("Default");
        optionsBuilder.UseNpgsql(connectionString);
        return new IdentityServerDbContext(optionsBuilder.Options);
    }
}