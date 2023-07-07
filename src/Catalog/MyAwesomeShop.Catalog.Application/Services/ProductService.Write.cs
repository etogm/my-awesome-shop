using Mapster;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Application.Dtos;
using MyAwesomeShop.Catalog.Domain;
using MyAwesomeShop.Shared.Application.Exceptions;

namespace MyAwesomeShop.Catalog.Application.Services;

internal partial class ProductService : IProductService
{
    private readonly ICatalogContext _context;

    public ProductService(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> CreateProductAsync(CreateProductRequest request)
    {
        var product = new Product(request.Name, request.Description, request.Price);

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product.Adapt<ProductDto>();
    }

    public async Task DeleteProductAsync(Guid id)
    {
        var product = await _context.Products.FindAsync(id);

        if (product == null)
        {
            return;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<ProductDto> UpdateProductAsync(UpdateProductRequest request)
    {
        var product = await _context.Products.FindAsync(request.Id)
            ?? throw new EntityNotFoundException(Product.EntityName, request.Id);

        product.Update(request.Name, request.Description, request.Price);

        return product.Adapt<ProductDto>();
    }
}
