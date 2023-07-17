using System.Text.Json;

using Microsoft.Extensions.Options;

using MyAwesomeShop.Shared;

using StackExchange.Redis;

namespace MyAwesomeShop.Basket.BasketFeature;

internal class RedisBasketRepository : IBasketRepository
{
    private readonly IDatabase _db;

    private readonly IServer _server;

    private readonly TimeSpan _expiry;

    public RedisBasketRepository(ConnectionMultiplexer connection, IOptions<BasketOptions> options)
    {
        _db = connection.GetDatabase();
        _server = connection.GetServer(options.Value.Connection);

        _expiry = options.Value.Expiry;
    }

    public async Task<IEnumerable<BasketProduct>> AddOrUpdateBasketAsync(Guid userId, BasketProduct product)
    {
        var result = await _db.StringSetAsync(GetKey(userId, product.Id), JsonSerializer.Serialize(product), _expiry);

        if (!result)
        {
            throw new RedisRepositoryException("Не удалось обновить корзину");
        }

        return await GetBasketAsync(userId) ?? throw new RedisRepositoryException("Не удалось получить корзину");
    }

    public async Task<BasketProduct?> GetProductFromBasketAsync(Guid userId, Guid productId)
    {
        var product = await _db.StringGetAsync(GetKey(userId, productId));

        if (product.IsNullOrEmpty)
        {
            return null;
        }

        return JsonSerializer.Deserialize<BasketProduct>(product.ToString());
    }

    public async Task<IEnumerable<BasketProduct>> GetBasketAsync(Guid userId)
    {
        var keys = _server.Keys(pattern: $"{userId}:*").ToArray();

        var values = await _db.StringGetAsync(keys);

        return values.Where(v => !v.IsNullOrEmpty).Select(v => JsonSerializer.Deserialize<BasketProduct>(v.ToString()))!;
    }

    public Task UpdateProductAsync(Guid id, string name, Money price)
    {
        var keys = _server.Keys(pattern: $"*:{id}");

        var tasks = keys.Select(async k =>
        {
            var value = await _db.StringGetAsync(k);
            var basketProduct = JsonSerializer.Deserialize<BasketProduct>(value.ToString());

            if (basketProduct == null)
            {
                return;
            }

            if (name == basketProduct.Name && price == basketProduct.Price)
            {
                return;
            }

            basketProduct.UpdateInformation(name, price);
            await _db.StringSetAsync(k, JsonSerializer.Serialize(basketProduct), _expiry);
        }).ToArray();

        Task.WaitAll(tasks);
        return Task.CompletedTask;
    }

    public Task DeleteProductFromBasketAsync(Guid userId, Guid productId)
    {
        return _db.StringGetDeleteAsync(GetKey(userId, productId));
    }

    private static string GetKey(Guid? userId = null, Guid? productId = null)
    {
        return (userId.HasValue, productId.HasValue) switch
        {
            (true, true) => $"{userId}:{productId}",
            (true, false) => $"{userId}:*",
            (false, true) => $"*:{productId}",
            _ => "*"
        };
    }
}

[Serializable]
internal class RedisRepositoryException : Exception
{
    public RedisRepositoryException(string? message) : base(message)
    {
    }
}