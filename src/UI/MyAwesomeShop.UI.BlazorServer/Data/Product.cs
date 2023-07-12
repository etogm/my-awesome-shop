using MyAwesomeShop.Shared;

namespace MyAwesomeShop.UI.BlazorServer.Data;

public class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Money Price { get; set; }
}
