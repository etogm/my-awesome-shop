using Mapster;

using Microsoft.EntityFrameworkCore;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Shared.Application.Extensions;
using MyAwesomeShop.Shared.Application.Messaging;

namespace MyAwesomeShop.Catalog.Application.Products;

public record GetProductsQuery(int CurrentPage, int PerPage) : IQuery<PaginatedList<ProductDto>>;

internal class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, PaginatedList<ProductDto>>
{
    private readonly ICatalogContext _context;

    public GetProductsQueryHandler(ICatalogContext context)
    {
        _context = context;
    }

    public Task<PaginatedList<ProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return _context.Products
            .AsNoTracking()
            .ProjectToType<ProductDto>()
            .ToPaginatedListAsync(request.CurrentPage, request.PerPage);
    }
}
