using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ISII.Web.Infrastructure.Data;

/// <summary>
/// Design-time factory for creating DbContext instances for EF Core tools.
/// This is used by EF Core tools for migrations and database updates.
///
/// Migrations are automatically APPLIED at runtime via MigrateAsync() in MiddlewareConfig.cs.
/// </summary>
public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  public AppDbContext CreateDbContext(string[] args)
  {
    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    optionsBuilder.UseSqlite("Data Source=ISII.db;Cache=Shared");

    return new AppDbContext(optionsBuilder.Options);
  }
}
