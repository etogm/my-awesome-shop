using MediatR;

using MyAwesomeShop.Shared.Domain;

namespace MyAwesomeShop.Shared.Application.Messaging;

public interface IDomainEventHandler<in TNotification> : INotificationHandler<TNotification> where TNotification : IDomainEvent
{
}
