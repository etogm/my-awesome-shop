using Grpc.Net.Client;

using MyAwesomeShop.Basket.Domain;
using MyAwesomeShop.Basket.Repositories;
using MyAwesomeShop.Catalog.Application.IntegrationEvents;
using MyAwesomeShop.Catalog.Grpc;
using MyAwesomeShop.Shared.Application.IntegrationEvent;

using static MyAwesomeShop.Catalog.Grpc.Catalog;

namespace MyAwesomeShop.Basket;

public static class WebApi
{
    public static WebApplication MapBasketWebApi(this WebApplication app)
    {
        app.MapGet("/api/v1/users/{userId}/products", (Guid userId, IBasketRepository repository, IEventBus pubSub) =>
        {
            return repository.GetBasketAsync(userId);
        });

        app.MapPost("/api/v1/users/{userId}/products", async (Guid userId, Guid productId, float quantity, IBasketRepository repository) =>
        {
            var product = await repository.GetProductFromBasketAsync(userId, productId);

            if (product != null)
            {
                product.Quantity += quantity;
            }
            else
            {
                var channel = GrpcChannel.ForAddress("https://localhost:62511");
                var catalogClient = new CatalogClient(channel);

                var response = await catalogClient.GetProductAsync(new GetProductRequest
                {
                    Id = productId.ToString()
                });

                product = new BasketProduct(
                    productId, 
                    response.Name,
                    quantity,
                    new Shared.Money(response.Price.Amount, (Shared.Currency)response.Price.Currency));
            }

            return await repository.AddOrUpdateBasketAsync(userId, product);
        });

        return app;
    }

    public static WebApplication UseBasketSub(this WebApplication app)
    {
        var eventBus = app.Services.GetRequiredService<IEventBus>();

        eventBus.SubscribeAsync<ProductUpdatedIntegrationEvent>();

        return app;
    }
}
