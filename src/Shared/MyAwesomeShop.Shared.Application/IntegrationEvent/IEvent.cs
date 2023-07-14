using System.Text.Json.Serialization;

namespace MyAwesomeShop.Shared.Application.IntegrationEvent;

public record IntegrationEvent 
{
	public IntegrationEvent()
	{
		Id = Guid.NewGuid();
		CreatedAt = DateTime.UtcNow;
	}

	[JsonConstructor]
	public IntegrationEvent(Guid id, DateTime createdAt)
	{
		Id = id;
		CreatedAt = createdAt;
	}

	public Guid Id { get; private init; }

	public DateTime CreatedAt { get; private init; }
}
