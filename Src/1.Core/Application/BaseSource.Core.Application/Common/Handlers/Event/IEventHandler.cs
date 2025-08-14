using MediatR;

namespace BaseSource.Core.Application.Common.Handlers.Event;

public interface IEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : DomainEvent
{
}

public abstract class EventHandler<TEvent> : IEventHandler<TEvent>
    where TEvent : DomainEvent
{
    public abstract Task Handle(TEvent notification, CancellationToken cancellationToken);
}
