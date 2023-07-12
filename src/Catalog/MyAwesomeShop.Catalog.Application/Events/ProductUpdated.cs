using MyAwesomeShop.Shared;
using MyAwesomeShop.Shared.Application.Events;

namespace MyAwesomeShop.Catalog.Application.Events;

public class ProductUpdated : IEvent
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Money Price { get; set; }
}
