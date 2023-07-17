using MyAwesomeShop.Catalog.Application.IntegrationEvents;
using MyAwesomeShop.Shared.Application.IntegrationEvent;

namespace MyAwesomeShop.Basket;

internal static class Subscriptions
{
    public static WebApplication UseBasketSubscriptions(this WebApplication app)
    {
        var eventBus = app.Services.GetRequiredService<IEventBus>();

        eventBus.SubscribeAsync<ProductUpdatedIntegrationEvent>();

        return app;
    }
}
