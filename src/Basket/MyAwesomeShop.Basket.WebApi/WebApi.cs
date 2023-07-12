using MyAwesomeShop.Basket.Domain;
using MyAwesomeShop.Basket.Repositories;
using MyAwesomeShop.Shared.Infrastructure.PubSub;

namespace MyAwesomeShop.Basket;

public static class WebApi
{
    public static WebApplication MapBasketWebApi(this WebApplication app)
    {
        app.MapGet("/api/v1/users/{userId}/products", async (Guid userId, IBasketRepository repository, IPubSub pubSub) =>
        {
            var userBasket = await repository.GetBasketAsync(userId);

            return userBasket?.Products ?? new HashSet<Product>();
        });

        app.MapPost("/api/v1/users/{userId}/products", async (Guid userId, Product product, IBasketRepository repository) =>
        {
            var userBasket = await repository.GetBasketAsync(userId);

            if (userBasket == null)
            {
                userBasket = new UserBasket(userId);
            }

            userBasket.AddOrUpdateProduct(product);
            return await repository.AddOrUpdateBasketAsync(userBasket);
        });

        return app;
    }

    public static WebApplication UseBasketSub(this WebApplication app)
    {
        var pubSub = app.Services.GetRequiredService<IPubSub>();

        return app;
    }
}
