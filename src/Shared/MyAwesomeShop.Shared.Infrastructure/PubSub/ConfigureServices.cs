using Microsoft.Extensions.DependencyInjection;

using MyAwesomeShop.Shared.Application.Events;

namespace MyAwesomeShop.Shared.Infrastructure.PubSub;

public static class ConfigureServices
{
    public static IServiceCollection AddPubSub(this IServiceCollection services, Action<PubSubOptions> options)
    {
        services.Configure(options);
        services.AddSingleton<IPubSub, RedisPubSub>();
        services.AddSingleton<IPubSubDispatcher, PubSubDispatcher>();

        return services;
    }

    public static IServiceCollection AddPubSubHandler<TMessage, THandler>(this IServiceCollection services)
        where THandler : class, IHandler<TMessage>
        where TMessage : class, IEvent
    {
        services.AddSingleton<IHandler<TMessage>, THandler>();
        return services;
    }
}
