using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using MyAwesomeShop.Catalog.Domain;

namespace MyAwesomeShop.Catalog.Infrastructure.Data.EntityTypeConfigurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.OwnsOne(p => p.Price, b =>
        {
            b.Property(p => p.Currency);
            b.Property(p => p.Amount);
        });
    }
}
