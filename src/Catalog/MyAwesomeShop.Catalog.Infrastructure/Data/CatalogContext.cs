using MediatR;

using Microsoft.EntityFrameworkCore;

using MyAwesomeShop.Catalog.Application.Abstractions;
using MyAwesomeShop.Catalog.Domain;
using MyAwesomeShop.Catalog.Infrastructure.Data.EntityTypeConfigurations;
using MyAwesomeShop.Shared.Domain;

namespace MyAwesomeShop.Catalog.Infrastructure.Data;

internal class CatalogContext : DbContext, ICatalogContext
{
    private readonly IMediator _mediator;

    public CatalogContext(IMediator mediator, DbContextOptions<CatalogContext> options) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog");

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entitiesWithEvents = ChangeTracker
            .Entries<Entity>()
            .Where(e => e.Entity.DomainEvents.Count > 0)
            .SelectMany(e => e.Entity.DomainEvents);

        foreach (var @event in entitiesWithEvents)
        {
            await _mediator.Publish(@event, cancellationToken);
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
