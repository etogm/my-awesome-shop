using MyAwesomeShop.Shared;
using MyAwesomeShop.Shared.Application.IntegrationEvent;

namespace MyAwesomeShop.Catalog.Application.IntegrationEvents;

public record ProductUpdatedIntegrationEvent(Guid ProductId, string Name, Money Price) : IntegrationEvent;
