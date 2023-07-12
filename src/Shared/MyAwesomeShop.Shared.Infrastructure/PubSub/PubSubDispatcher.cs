using Microsoft.Extensions.DependencyInjection;

using MyAwesomeShop.Shared.Application.Events;

namespace MyAwesomeShop.Shared.Infrastructure.PubSub;

internal class PubSubDispatcher : IPubSubDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public PubSubDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task DispatchAsync<TMessage>(TMessage message, CancellationToken token) where TMessage : IEvent
    {
        var handle = _serviceProvider.GetRequiredService<IHandler<TMessage>>();
        return handle.HandleAsync(message);
    }
}
