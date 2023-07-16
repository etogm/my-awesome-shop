using MyAwesomeShop.Basket.Repositories;
using MyAwesomeShop.Catalog.Application.IntegrationEvents;
using MyAwesomeShop.Shared.Application.IntegrationEvent;

namespace MyAwesomeShop.Basket.Application;

internal class ProductUpdatedHandler : IIntegrationEventHandler<ProductUpdatedIntegrationEvent>
{
    private readonly IBasketRepository _repository;

    public ProductUpdatedHandler(IBasketRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(ProductUpdatedIntegrationEvent message)
    {
        return _repository.UpdateProductAsync(message.ProductId, message.Name, message.Price);
    }
}
