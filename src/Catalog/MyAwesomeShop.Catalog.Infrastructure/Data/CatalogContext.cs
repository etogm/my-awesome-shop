using Microsoft.EntityFrameworkCore;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Domain;
using MyAwesomeShop.Catalog.Infrastructure.Data.EntityTypeConfigurations;

namespace MyAwesomeShop.Catalog.Infrastructure.Data;

public class CatalogContext : DbContext, ICatalogContext
{
    public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }
}
