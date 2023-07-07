using Microsoft.EntityFrameworkCore;

using MyAwesomeShop.Catalog.Domain;

namespace MyAwesomeShop.Catalog.Application.Abstractions;

public interface ICatalogContext
{
    DbSet<Product> Products { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
