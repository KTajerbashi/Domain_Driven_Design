using BaseSource.Core.Domain.Common.Event;

namespace BaseSource.Core.Domain.Common.Aggregate;

// Core interface for all entities
public interface IEntity<TId> where TId : notnull
{
    TId Id { get; }

    // Domain event collection for all entities
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}

