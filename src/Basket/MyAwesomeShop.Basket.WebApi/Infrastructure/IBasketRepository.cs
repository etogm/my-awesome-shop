﻿using MyAwesomeShop.Basket.Domain;
using MyAwesomeShop.Shared;

namespace MyAwesomeShop.Basket.Repositories;

public interface IBasketRepository
{
    Task<IEnumerable<BasketProduct>> GetBasketAsync(Guid userId);

    Task<BasketProduct?> GetProductFromBasketAsync(Guid userId, Guid productId);

    Task<IEnumerable<BasketProduct>> AddOrUpdateBasketAsync(Guid userId, BasketProduct product);

    Task UpdateProductAsync(Guid id, string name, Money price);
}
