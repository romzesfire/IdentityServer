using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.DAL;

public class IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : IdentityDbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder
            .UseSnakeCaseNamingConvention();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("public");
        base.OnModelCreating(modelBuilder);

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            var currentTableName = modelBuilder.Entity(entity.Name).Metadata.GetDefaultTableName();
            if (currentTableName.Contains("<"))
            {
                currentTableName = currentTableName.Split('<')[0];
            }
            modelBuilder.Entity(entity.Name).ToTable(ToUnderscoreCase(currentTableName));
        }
    }
    
    private static string ToUnderscoreCase(string str)
    {
        return string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + x : x.ToString())).ToLower();
    }
}