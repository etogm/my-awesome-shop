using Mapster;

using Microsoft.EntityFrameworkCore;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Application.Dtos;
using MyAwesomeShop.Shared.Application;

namespace MyAwesomeShop.Catalog.Application.Services;

internal partial class ProductService : IProductService
{
    public async Task<ProductDto?> GetProductAsync(Guid id)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id);

        return product?.Adapt<ProductDto>();
    }

    public Task<PaginatedList<ProductDto>> GetProductsAsync(int currentPage, int perPage)
    {
        return PaginatedList<ProductDto>
            .CreateAsync(_context.Products.ProjectToType<ProductDto>(), currentPage, perPage);
    }
}
