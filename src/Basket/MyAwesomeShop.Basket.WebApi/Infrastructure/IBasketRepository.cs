using MyAwesomeShop.Basket.Domain;

namespace MyAwesomeShop.Basket.Repositories;

public interface IBasketRepository
{
    Task<UserBasket?> GetBasketAsync(Guid userId);

    Task<UserBasket> AddOrUpdateBasketAsync(UserBasket basket);
}
