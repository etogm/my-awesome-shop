namespace MyAwesomeProject.Shared;

public class Money
{
    public decimal Amount { get; set; }

    public Currency Currency { get; set; }
}

public enum Currency
{
    RUB
}
