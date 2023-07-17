using Microsoft.Extensions.DependencyInjection;

using MyAwesomeShop.Shared.Application.IntegrationEvent;

namespace MyAwesomeShop.Shared.Infrastructure.EventBus;

internal class EventBusDispatcher : IEventBusDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventBusDispatcher(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task DispatchAsync<TMessage>(TMessage message, CancellationToken token) where TMessage : IntegrationEvent
    {
        using var scope = _serviceProvider.CreateScope();
        var handle = scope.ServiceProvider.GetRequiredService<IIntegrationEventHandler<TMessage>>();
        return handle.Handle(message);
    }
}
