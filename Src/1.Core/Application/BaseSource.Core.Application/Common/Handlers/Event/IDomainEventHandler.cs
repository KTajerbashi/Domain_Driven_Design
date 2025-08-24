using BaseSource.Core.Domain.Common.Aggregate;
using MediatR;

namespace BaseSource.Core.Application.Common.Handlers.Event;

public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : DomainEvent
{
}

public abstract class DomainEventHandler<TEvent> : IDomainEventHandler<TEvent>
    where TEvent : DomainEvent
{
    protected readonly ProviderFactory Factory;
    protected DomainEventHandler(ProviderFactory factory)
    {
        Factory = factory;
    }
    public abstract Task Handle(TEvent notification, CancellationToken cancellationToken);
}

