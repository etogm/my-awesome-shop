using Microsoft.Extensions.DependencyInjection;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Application.Services;

namespace MyAwesomeShop.Catalog.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddCatalogApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}

