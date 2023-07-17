using Hellang.Middleware.ProblemDetails.Mvc;

using MyAwesomeShop.Basket.BasketFeature;
using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Application.IntegrationEvents;
using MyAwesomeShop.Shared.Application;
using MyAwesomeShop.Shared.Infrastructure.EventBus;
using MyAwesomeShop.Shared.WebApi;

using Refit;

using StackExchange.Redis;

namespace MyAwesomeShop.Basket;

public static class ConfigureServices
{
    public static IServiceCollection AddBasketWebApi(this IServiceCollection services, IConfiguration configuration, IHostEnvironment env)
    {
        services.AddCors(options => options.AddInternalDefaultPolicy(configuration["GatewayUri"]!));
        services.AddWebApi(env);
        services.AddWebApiSwagger();

        return services;
    }

    public static IServiceCollection AddBasketInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddRefitClient<ICatalogQueryService>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(configuration["Uris:Catalog"]!));

        services.Configure<BasketOptions>(configuration.GetSection("BasketOptions"));
        services.AddScoped<IBasketRepository, RedisBasketRepository>();
        services.AddSingleton(provider =>
        {
            return ConnectionMultiplexer.Connect(configuration["BasketOptions:Connection"]!);
        });

        services.AddEventBus(options =>
        {
            options.Configuration = configuration["RedisOptions:Connection"]!;
        });

        return services;
    }

    public static IServiceCollection AddBasketApplication(this IServiceCollection services)
    {
        services.AddMapper();
        services.AddIntegrationEventHandler<ProductUpdatedIntegrationEvent, ProductUpdatedHandler>();
        return services;
    }
}