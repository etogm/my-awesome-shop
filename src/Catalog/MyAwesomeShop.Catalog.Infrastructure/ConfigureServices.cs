using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Infrastructure.Data;

namespace MyAwesomeShop.Catalog.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<CatalogContext>(o => o.UseInMemoryDatabase(Guid.NewGuid().ToString()));
        //services.AddDbContext<CatalogContext>(o => o.UseNpgsql().UseSnakeCaseNamingConvention());
        services.AddScoped<ICatalogContext>(provider => provider.GetRequiredService<CatalogContext>());

        return services;
    }
}
