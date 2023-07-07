using MyAwesomeShop.Catalog.Application.Dtos;
using MyAwesomeShop.Shared.Application;

namespace MyAwesomeShop.Catalog.Application.Abstractions;

public interface IProductService
{
    Task<PaginatedList<ProductDto>> GetProductsAsync(int currentPage, int perPage);

    Task<ProductDto?> GetProductAsync(Guid id);

    Task<ProductDto> CreateProductAsync(CreateProductRequest request);

    Task<ProductDto> UpdateProductAsync(UpdateProductRequest request);

    Task DeleteProductAsync(Guid id);
}
