using System.Text.Json;

using MyAwesomeShop.Basket.Domain;

using StackExchange.Redis;

namespace MyAwesomeShop.Basket.Repositories;

internal class RedisBasketRepository : IBasketRepository
{
    private readonly IDatabase _db;

    public RedisBasketRepository(ConnectionMultiplexer connection)
    {
        _db = connection.GetDatabase();
    }

    public async Task<UserBasket> AddOrUpdateBasketAsync(UserBasket basket)
    {
        var result = await _db.StringSetAsync(basket.Id.ToString(), JsonSerializer.Serialize(basket));

        if (!result)
        {
            throw new RedisRepositoryException("Не удалось обновить корзину");
        }

        return await GetBasketAsync(basket.Id) ?? throw new RedisRepositoryException("Не удалось получить корзину");
    }

    public async Task<UserBasket?> GetBasketAsync(Guid userId)
    {
        var basket = await _db.StringGetAsync(userId.ToString());

        if (basket.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<UserBasket>(basket.ToString());
    }
}

[Serializable]
internal class RedisRepositoryException : Exception
{
    public RedisRepositoryException(string? message) : base(message)
    {
    }
}