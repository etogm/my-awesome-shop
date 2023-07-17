using Mapster;

using Microsoft.EntityFrameworkCore;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Shared.Application.Messaging;

namespace MyAwesomeShop.Catalog.Application.Products;

public record GetProductQuery(Guid Id) : IQuery<ProductDto>;

internal class GetProductQueryHandler : IQueryHandler<GetProductQuery, ProductDto?>
{
    private readonly ICatalogContext _context;

    public GetProductQueryHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<ProductDto?> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context.Products
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        return product?.Adapt<ProductDto>();
    }
}
