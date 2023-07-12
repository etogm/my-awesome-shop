using MyAwesomeShop.Shared;
using MyAwesomeShop.Shared.Domain;

namespace MyAwesomeShop.Catalog.Domain;

public record ProductUpdated(Guid Id, string Name, Money Price) : IDomainEvent;
