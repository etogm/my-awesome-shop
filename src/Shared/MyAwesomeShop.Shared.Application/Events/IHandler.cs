namespace MyAwesomeShop.Shared.Application.Events;

public interface IHandler<T> where T : IEvent
{
    Task HandleAsync(T message);
}
