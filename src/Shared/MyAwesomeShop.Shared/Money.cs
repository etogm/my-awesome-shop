namespace MyAwesomeShop.Shared;

public class Money
{
    private Money()
    {
    }

    public Money(decimal amount, Currency currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public decimal Amount { get; set; }

    public Currency Currency { get; set; }

    public override string ToString()
    {
        return $"{Amount} {Currency}";
    }
}

public enum Currency
{
    RUB
}
