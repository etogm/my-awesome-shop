using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Shared.Application.Messaging;

namespace MyAwesomeShop.Catalog.Application.Products;

public record DeleteProductCommand(Guid Id) : ICommand;

internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
{
    private readonly ICatalogContext _context;

    public DeleteProductCommandHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _context.Products.FindAsync(request.Id);

        if (product == null)
        {
            return;
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }
}
