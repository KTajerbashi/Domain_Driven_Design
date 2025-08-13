namespace BaseSource.Core.Domain.Common.Aggregate;

// Base aggregate root implementation
public abstract class AggregateRoot<TId> : AuditableEntity<TId>, IAggregateRoot<TId>
    where TId : notnull
{
    // Strongly-typed domain event addition
    protected void AddDomainEvent<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        base.AddDomainEvent(domainEvent);
    }
}

// Convenience class for long ID aggregates
public abstract class AggregateRoot : AggregateRoot<long>
{

}