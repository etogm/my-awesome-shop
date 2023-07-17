using MyAwesomeShop.Catalog.Application.Products;

using Refit;

namespace MyAwesomeShop.Catalog.Application.Abstractions;

public interface ICatalogQueryService
{
    [Get("/api/v1/products/{productId}")]
    Task<ProductDto> GetProductAsync(Guid productId);
}
