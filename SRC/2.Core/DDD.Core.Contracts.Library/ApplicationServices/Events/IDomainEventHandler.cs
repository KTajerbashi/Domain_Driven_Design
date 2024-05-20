
using DDD.Core.Domain.Library.Events;

namespace DDD.Core.Contracts.Library.ApplicationServices.Events;

public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent Event);
}