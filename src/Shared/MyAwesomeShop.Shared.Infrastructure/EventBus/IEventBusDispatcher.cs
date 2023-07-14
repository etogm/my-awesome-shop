using MyAwesomeShop.Shared.Application.IntegrationEvent;

namespace MyAwesomeShop.Shared.Infrastructure.EventBus;

public interface IEventBusDispatcher
{
    Task DispatchAsync<TMessage>(TMessage message, CancellationToken token = default) where TMessage : IntegrationEvent;
}
