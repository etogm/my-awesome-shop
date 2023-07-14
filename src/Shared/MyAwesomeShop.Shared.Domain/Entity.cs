namespace MyAwesomeShop.Shared.Domain;

public abstract class Entity : EntityBase<Guid>
{
    public Entity()
    {
        Id = Guid.NewGuid();
    }

    public Entity(Guid id)
    {
        Id = id;
    }
}
