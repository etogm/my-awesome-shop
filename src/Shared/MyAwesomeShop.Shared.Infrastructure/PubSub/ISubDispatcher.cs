using MyAwesomeShop.Shared.Application.Events;

namespace MyAwesomeShop.Shared.Infrastructure.PubSub;

public interface IPubSubDispatcher
{
    Task DispatchAsync<TMessage>(TMessage message, CancellationToken token = default) where TMessage : IEvent;
}
