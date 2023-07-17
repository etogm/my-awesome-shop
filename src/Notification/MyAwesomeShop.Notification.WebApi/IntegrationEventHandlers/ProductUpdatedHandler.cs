using Microsoft.AspNetCore.SignalR;

using MyAwesomeShop.Catalog.Application.IntegrationEvents;
using MyAwesomeShop.Notification.WebApi.Hubs;
using MyAwesomeShop.Shared.Application.IntegrationEvent;

internal class ProductUpdatedHandler : IIntegrationEventHandler<ProductUpdatedIntegrationEvent>
{
    private readonly IHubContext<ProductHub> _hub;

    public ProductUpdatedHandler(IHubContext<ProductHub> hub)
    {
        _hub = hub;
    }

    public async Task Handle(ProductUpdatedIntegrationEvent message)
    {
        await _hub.Clients.Group(ProductHub.GetProductGroup(message.ProductId)).SendAsync(ProductHubConstants.ProductUpdated, message);
    }
}