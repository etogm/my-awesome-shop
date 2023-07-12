namespace MyAwesomeShop.Shared.Domain;

public abstract class EntityBase<TId>
{
    private List<IDomainEvent> _events;

    public TId Id { get; set; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents { get { return _events; } }

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        if (_events == null)
        {
            _events = new List<IDomainEvent>();
        }

        _events.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _events.Clear();
    }
}
