using Mapster;

using Microsoft.EntityFrameworkCore;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Application.Dtos;
using MyAwesomeShop.Shared.Application;

namespace MyAwesomeShop.Catalog.Application.Services;

internal partial class ProductService : IProductService
{
    public Task<ProductDto?> GetProductAsync(Guid id)
    {
        return _context.Products
            .ProjectToType<ProductDto>()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public Task<PaginatedList<ProductDto>> GetProductsAsync(int currentPage, int perPage)
    {
        return PaginatedList<ProductDto>.CreateAsync(_context.Products.ProjectToType<ProductDto>(), currentPage, perPage);
    }
}
