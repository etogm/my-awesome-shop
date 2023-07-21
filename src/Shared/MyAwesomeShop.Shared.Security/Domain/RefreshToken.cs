namespace MyAwesomeShop.Shared.Security.Domain;

public class RefreshToken
{
    private RefreshToken() { }

    public RefreshToken(DateTime validTo)
    {
        Value = Guid.NewGuid().ToString();
        ValidTo = validTo;
    }

    public string Value { get; set; }

    public DateTime ValidTo { get; set; }
}
