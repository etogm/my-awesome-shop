using MyAwesomeShop.Shared.Application.Events;

namespace MyAwesomeShop.Shared.Infrastructure.PubSub;

public interface IPubSub
{
    Task PublishAsync<T>(T message) where T : IEvent;

    Task SubscribeAsync<T>() where T : IEvent;
}
