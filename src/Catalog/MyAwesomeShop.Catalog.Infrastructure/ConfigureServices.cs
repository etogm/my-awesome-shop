using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Infrastructure.Data;
using MyAwesomeShop.Shared.Infrastructure.EventBus;

namespace MyAwesomeShop.Catalog.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, Action<CatalogInfrastructureOptions> options)
    {
        var infraOptions = new CatalogInfrastructureOptions();
        options(infraOptions);

        services.AddDbContext<CatalogContext>(options =>
        {
            options
                .UseNpgsql(
                    infraOptions.CatalogDbConnectionString,
                    options => options.MigrationsHistoryTable("catalog_migrations_history", "public"))
                .UseSnakeCaseNamingConvention();
        });
        services.AddScoped<ICatalogContext>(provider => provider.GetRequiredService<CatalogContext>());

        services.AddEventBus(options =>
        {
            options.Connection = infraOptions.EventBusConnectionString;
        });

        return services;
    }
}

public class CatalogInfrastructureOptions
{
    public string CatalogDbConnectionString { get; set; }

    public string EventBusConnectionString { get; set; }
}