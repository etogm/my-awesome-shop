using MyAwesomeShop.Shared.WebApi;

namespace MyAwesomeShop.Catalog.WebApi;

public static class ConfigureServices
{
    public static IServiceCollection AddCatalogWebApi(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddCors(options => options.AddInternalDefaultPolicy(configuration["GatewayUri"]!));
        services.AddWebApi(env);
        services.AddWebApiSwagger();

        return services;
    }
}
