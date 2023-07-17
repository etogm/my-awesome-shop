using Mapster;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Domain;
using MyAwesomeShop.Shared;
using MyAwesomeShop.Shared.Application.Exceptions;
using MyAwesomeShop.Shared.Application.Messaging;

namespace MyAwesomeShop.Catalog.Application.Products;

public record UpdateProductCommand(Guid Id, string Name, string Description, Money Price) : ICommand<ProductDto>;

internal class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, ProductDto>
{
    private readonly ICatalogContext _context;

    public UpdateProductCommandHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken)
            ?? throw new EntityNotFoundException(Product.EntityName, request.Id);

        product.Update(request.Name, request.Description, request.Price);

        await _context.SaveChangesAsync(cancellationToken);

        return product.Adapt<ProductDto>();
    }
}
