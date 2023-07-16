using Microsoft.Extensions.DependencyInjection;

using MyAwesomeShop.Shared.Application.IntegrationEvent;

namespace MyAwesomeShop.Shared.Infrastructure.EventBus;

public static class ConfigureServices
{
    public static IServiceCollection AddEventBus(this IServiceCollection services, Action<EventBusOptions> options)
    {
        services.Configure(options);
        services.AddSingleton<IEventBus, RedisEventBus>();
        services.AddSingleton<IEventBusDispatcher, EventBusDispatcher>();

        return services;
    }

    public static IServiceCollection AddIntegrationEventHandler<TMessage, THandler>(this IServiceCollection services)
        where THandler : class, IIntegrationEventHandler<TMessage>
        where TMessage : IntegrationEvent
    {
        services.AddScoped<IIntegrationEventHandler<TMessage>, THandler>();
        return services;
    }
}
