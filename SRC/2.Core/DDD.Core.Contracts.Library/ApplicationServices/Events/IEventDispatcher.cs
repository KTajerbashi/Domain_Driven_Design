using DDD.Core.Domain.Library.Events;

namespace DDD.Core.Contracts.Library.ApplicationServices.Events;
/// <summary>
/// این جهت پیاده سازی الگوی 
/// Mediate R 
/// میباشد
/// </summary>
public interface IEventDispatcher
{
    Task PublishDomainEventAsync<TDomainEvent>(TDomainEvent @event) where TDomainEvent : class, IDomainEvent;
}