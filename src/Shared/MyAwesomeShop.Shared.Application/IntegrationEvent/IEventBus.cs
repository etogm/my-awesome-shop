namespace MyAwesomeShop.Shared.Application.IntegrationEvent;

public interface IEventBus
{
    Task PublishAsync<T>(T message) where T : IntegrationEvent;

    Task SubscribeAsync<T>() where T : IntegrationEvent;
}
