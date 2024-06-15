
using DDD.Core.Domain.Library.Events;

namespace DDD.Core.Contracts.Library.Data.Commands;
/// <summary>
/// در صورت نیاز به ذخیره و بازیابی 
/// event
/// ها از این اینترفیس استفاده می‌شود.
/// </summary>
public interface IDomainEventStore
{
    void Save<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent;
    Task SaveAsync<TEvent>(string aggregateName, string aggregateId, IEnumerable<TEvent> events) where TEvent : IDomainEvent;
}

