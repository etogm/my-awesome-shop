using Microsoft.Extensions.DependencyInjection;

using MyAwesomeShop.Shared.Application;

namespace MyAwesomeShop.Catalog.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
    {
        services.AddMapper();
        services.AddMessaging(typeof(ConfigureServices));

        return services;
    }
}

