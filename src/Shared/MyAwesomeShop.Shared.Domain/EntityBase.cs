namespace MyAwesomeShop.Shared.Domain;

public abstract class EntityBase<TId>
{
    public TId Id { get; set; }
}
