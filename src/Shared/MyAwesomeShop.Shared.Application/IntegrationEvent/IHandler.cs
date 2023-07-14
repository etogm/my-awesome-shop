namespace MyAwesomeShop.Shared.Application.IntegrationEvent;

public interface IIntegrationEventHandler<T> where T : IntegrationEvent
{
    Task HandleAsync(T message);
}
