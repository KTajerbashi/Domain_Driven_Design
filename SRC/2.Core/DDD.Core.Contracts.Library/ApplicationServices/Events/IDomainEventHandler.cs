using DDD.Core.Domain.Library.Events;

namespace DDD.Core.Contracts.Library.ApplicationServices.Events;


/// <summary>
/// 
/// </summary>
/// <typeparam name="TDomainEvent"></typeparam>
public interface IDomainEventHandler<TDomainEvent> where TDomainEvent : IDomainEvent
{
    Task Handle(TDomainEvent Event);
}