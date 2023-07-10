namespace MyAwesomeProject.Shared;

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
}

public enum Currency
{
    RUB
}
