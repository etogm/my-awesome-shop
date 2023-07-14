using MyAwesomeShop.Shared;
using MyAwesomeShop.Shared.Domain;

namespace MyAwesomeShop.Basket.Domain;

public class BasketProduct : Entity
{
    private BasketProduct()
    {
        // For mapster
    }

    public BasketProduct(Guid id, string name, float quantity, Money price) : base(id)
    {
        Name = name;
        Quantity = quantity;
        Price = price;
    }

    public string Name { get; private set; }

    public float Quantity { get; set; }

    public Money Price { get; private set; }

    public BasketProduct UpdateInformation(string name, Money price)
    {
        Name = name;
        Price = price;

        return this;
    }
}