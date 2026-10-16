using Ardalis.GuardClauses;
using ISII.Web.Infrastructure.Data;
using ISII.Web.Infrastructure.Data.Queries;
using ISII.Web.ProductFeatures.List;
using Microsoft.EntityFrameworkCore;

namespace ISII.Web.Infrastructure;
public static class InfrastructureServiceExtensions
{
  public static IServiceCollection AddInfrastructureServices(
    this IServiceCollection services,
    ConfigurationManager config,
    ILogger logger)
  {
    string? connectionString = config.GetConnectionString("AppDb");
    Guard.Against.Null(connectionString, "AppDb connection string is required.");

    services.AddScoped<EventDispatchInterceptor>();
    services.AddScoped<IDomainEventDispatcher, MediatorDomainEventDispatcher>();

    services.AddDbContext<AppDbContext>((provider, options) =>
    {
      var eventDispatchInterceptor = provider.GetRequiredService<EventDispatchInterceptor>();

      options.UseSqlite(connectionString);
      options.AddInterceptors(eventDispatchInterceptor);
    });

    services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>))
           .AddScoped(typeof(IReadRepository<>), typeof(EfRepository<>))
           .AddScoped<IListProductsQueryService, ListProductsQueryService>();

    logger.LogInformation("{Project} services registered", "Infrastructure");

    return services;
  }
}
