using Microsoft.Extensions.Logging;

using MyAwesomeShop.Catalog.Application.IntegrationEvents;
using MyAwesomeShop.Catalog.Domain;
using MyAwesomeShop.Shared.Application.IntegrationEvent;
using MyAwesomeShop.Shared.Application.Messaging;
using MyAwesomeShop.Shared.Domain;

namespace MyAwesomeShop.Catalog.Application.DomainEventHandlers;

internal sealed class ProductUpdatedHandler<TNotification> : IDomainEventHandler<ProductUpdated>
    where TNotification : IDomainEvent
{
    private readonly IEventBus _eventBus;

    public ProductUpdatedHandler(IEventBus eventBus)
    {
        _eventBus = eventBus;
    }

    public async Task Handle(ProductUpdated notification, CancellationToken cancellationToken)
    {
        await _eventBus.PublishAsync(new ProductUpdatedIntegrationEvent(notification.Id, notification.Name, notification.Price));
    }
}
