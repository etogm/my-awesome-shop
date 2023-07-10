namespace MyAwesomeShop.Shared.Application.Exceptions;

public class MyAwesomeShopException : Exception
{
    public MyAwesomeShopException(string message) : base(message)
    {
        Title = "Упс..";
    }

    protected MyAwesomeShopException(string title, string message) : base(message)
    {
        Title = title;
    }

    public string Title { get; init; }
}
