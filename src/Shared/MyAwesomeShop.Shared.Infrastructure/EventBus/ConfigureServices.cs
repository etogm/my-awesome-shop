using Microsoft.Extensions.DependencyInjection;

using MyAwesomeShop.Shared.Application.IntegrationEvent;

namespace MyAwesomeShop.Shared.Infrastructure.EventBus;

public static class ConfigureServices
{
    public static IServiceCollection AddPubSub(this IServiceCollection services, Action<EventBusOptions> options)
    {
        services.Configure(options);
        services.AddSingleton<IEventBus, RedisEventBus>();
        services.AddSingleton<IEventBusDispatcher, EventBusDispatcher>();

        return services;
    }

    public static IServiceCollection AddPubSubHandler<TMessage, THandler>(this IServiceCollection services)
        where THandler : class, IIntegrationEventHandler<TMessage>
        where TMessage : IntegrationEvent
    {
        services.AddSingleton<IIntegrationEventHandler<TMessage>, THandler>();
        return services;
    }
}
