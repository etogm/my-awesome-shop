namespace MyAwesomeShop.Basket.Domain;

public class UserBasket
{
    public UserBasket(Guid id)
    {
        Id = id;
        UpdatedAt = DateTime.Now;
        Products = new List<Product>(); ;
    }

    public Guid Id { get; init; }

    public DateTime UpdatedAt { get; private set; }

    public ICollection<Product> Products { get; init; }

    public UserBasket AddOrUpdateProduct(Product newProduct)
    {
        var product = Products.FirstOrDefault(p => p.Id == newProduct.Id);

        if (product == null)
        {
            Products.Add(newProduct);
        }
        else
        {
            product.Quantity += newProduct.Quantity;
        }

        UpdatedAt = DateTime.Now;

        return this;
    }
}
