using FluentValidation;

using Mapster;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Domain;
using MyAwesomeShop.Shared;
using MyAwesomeShop.Shared.Application.Messaging;

namespace MyAwesomeShop.Catalog.Application.Products;

public record CreateProductCommand(string Name, string Description, Money Price) : ICommand<ProductDto>;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).MaximumLength(24);
    }
}

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, ProductDto>
{
    private readonly ICatalogContext _context;

    public CreateProductCommandHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Description, request.Price);

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return product.Adapt<ProductDto>();
    }
}
