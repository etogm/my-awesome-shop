using System.Text.Json.Serialization;

namespace MyAwesomeShop.Shared.Domain;

public abstract class EntityBase<TId>
{
    private readonly List<IDomainEvent> _events = new();

    public TId Id { get; set; }

    [JsonIgnore]
    public IReadOnlyCollection<IDomainEvent> DomainEvents { get { return _events; } }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _events.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _events.Clear();
    }
}
