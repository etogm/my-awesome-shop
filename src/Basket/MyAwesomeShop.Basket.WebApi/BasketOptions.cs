namespace MyAwesomeShop.Basket;

internal class BasketOptions
{
    public string Connection { get; set; }

    public TimeSpan Expiry { get; set; } = TimeSpan.FromDays(1);
}
