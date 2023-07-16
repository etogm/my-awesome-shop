using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Infrastructure.Data;
using MyAwesomeShop.Shared.Infrastructure.EventBus;

namespace MyAwesomeShop.Catalog.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CatalogContext>(options =>
        {
            options
                .UseNpgsql(configuration.GetConnectionString("CatalogDB"))
                .UseSnakeCaseNamingConvention();
        });
        services.AddScoped<ICatalogContext>(provider => provider.GetRequiredService<CatalogContext>());

        services.AddEventBus(options =>
        {
            options.Configuration = "localhost:6379";
        });

        return services;
    }

    public static IApplicationBuilder UseCatalogInfrastructure(this IApplicationBuilder app)
    {
        return app;
    }
}
