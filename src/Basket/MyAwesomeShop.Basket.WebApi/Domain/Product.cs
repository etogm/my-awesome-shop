using MyAwesomeShop.Shared;

namespace MyAwesomeShop.Basket.Domain;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public float Quantity { get; set; }

    public Money Price { get; set; }
}