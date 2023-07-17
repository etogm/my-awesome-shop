using MyAwesomeShop.Basket.BasketFeature;
using MyAwesomeShop.Catalog.Application.Abstractions;

namespace MyAwesomeShop.Basket;

public static class WebApi
{
    public static WebApplication MapBasketWebApi(this WebApplication app)
    {
        app.MapGet("/api/v1/users/{userId}/products", (Guid userId, IBasketRepository repository) =>
        {
            return repository.GetBasketAsync(userId);
        });

        app.MapPost("/api/v1/users/{userId}/products",async (Guid userId, Guid productId, float quantity, ICatalogQueryService catalogService, IBasketRepository repository) =>
        {
            var product = await repository.GetProductFromBasketAsync(userId, productId);

            if (product != null)
            {
                product.Quantity += quantity;
            }
            else
            {
                var productFromCatalog = await catalogService.GetProductAsync(productId);

                product = new BasketProduct(
                    productId,
                    productFromCatalog.Name,
                    quantity,
                    productFromCatalog.Price);
            }

            return await repository.AddOrUpdateBasketAsync(userId, product);
        });

        app.MapDelete("/api/v1/users/{userId}/products", async (Guid userId, Guid productId, IBasketRepository repository) =>
        {
            await repository.DeleteProductFromBasketAsync(userId, productId);
        });

        return app;
    }
}
