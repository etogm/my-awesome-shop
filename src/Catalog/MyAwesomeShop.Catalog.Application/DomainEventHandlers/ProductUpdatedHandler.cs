using Microsoft.Extensions.Logging;

using MyAwesomeShop.Catalog.Application.IntegrationEvents;
using MyAwesomeShop.Catalog.Domain;
using MyAwesomeShop.Shared.Application.IntegrationEvent;
using MyAwesomeShop.Shared.Application.Messaging;
using MyAwesomeShop.Shared.Domain;

namespace MyAwesomeShop.Catalog.Application.DomainEventHandlers;

public sealed class ProductUpdatedHandler<TNotification> : IDomainEventHandler<ProductUpdated>
    where TNotification : IDomainEvent
{
    private readonly ILogger _logger;

    private readonly IEventBus _eventBus;

    public ProductUpdatedHandler(ILogger<ProductUpdatedHandler<TNotification>> looger)
    {
        _logger = looger;
    }

    public async Task Handle(ProductUpdated notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Notification: " + notification.ToString());
        await _eventBus.PublishAsync(new ProductUpdatedIntegrationEvent(notification.Id, notification.Name, notification.Price));
    }
}
