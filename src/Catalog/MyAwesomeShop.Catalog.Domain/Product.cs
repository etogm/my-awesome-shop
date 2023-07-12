using MyAwesomeShop.Shared;
using MyAwesomeShop.Shared.Domain;

namespace MyAwesomeShop.Catalog.Domain;

public class Product : Entity
{
    private Product()
    {
        // For EF
    }

    public Product(string name, Money price) : this(name, string.Empty, price)
    {
    }

    public Product(string name, string description, Money price) : base()
    {
        Name = name;
        Description = description;
        Price = price;
    }

    public string Name { get; set; }

    public string Description { get; set; }

    public Money Price { get; set; }

    public static string EntityName = "Товар";

    public Product Update(string name, string description, Money price)
    {
        Name = name;
        Description = description;
        Price = price;

        AddDomainEvent(new ProductUpdated(Id, Name, Price));

        return this;
    }
}
