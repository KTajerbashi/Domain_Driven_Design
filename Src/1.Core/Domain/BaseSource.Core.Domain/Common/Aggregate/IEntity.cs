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

// Base entity implementation
public abstract class Entity<TId> : IEntity<TId>, IEquatable<Entity<TId>>
    where TId : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new();

    public TId Id { get; protected init; }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected Entity(TId id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    // Equality implementation
    public bool Equals(Entity<TId>? other)
    {
        return Equals((object?)other);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Entity<TId> other)
            return false;

        if (ReferenceEquals(this, other))
            return true;

        if (GetType() != other.GetType())
            return false;

        return Id.Equals(other.Id);
    }

    public override int GetHashCode()
    {
        return (GetType().ToString() + Id).GetHashCode();
    }

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right)
    {
        return !Equals(left, right);
    }
}

// Interface for auditable entities
public interface IAuditableEntity<TId> : IEntity<TId> where TId : notnull
{
    DateTime CreatedAt { get; }
    TId CreatedBy { get; }
    DateTime? LastModifiedAt { get; }
    TId? LastModifiedBy { get; }
}

// Base auditable entity implementation
public abstract class AuditableEntity<TId> : Entity<TId>, IAuditableEntity<TId>
    where TId : notnull
{
    public DateTime CreatedAt { get; protected set; }
    public TId CreatedBy { get; protected set; }
    public DateTime? LastModifiedAt { get; protected set; }
    public TId? LastModifiedBy { get; protected set; }

    protected AuditableEntity(TId id) : base(id)
    {
    }

    protected void SetCreationAudit(DateTime createdAt, TId createdBy)
    {
        CreatedAt = createdAt;
        CreatedBy = createdBy;
    }

    protected void SetModificationAudit(DateTime modifiedAt, TId modifiedBy)
    {
        LastModifiedAt = modifiedAt;
        LastModifiedBy = modifiedBy;
    }
}

// Interface for aggregate roots
public interface IAggregateRoot<TId> : IAuditableEntity<TId> where TId : notnull
{
    int Version { get; }
    void IncrementVersion();
}

// Base aggregate root implementation
public abstract class AggregateRoot<TId> : AuditableEntity<TId>, IAggregateRoot<TId>
    where TId : notnull
{
    public int Version { get; private set; }

    protected AggregateRoot(TId id) : base(id)
    {
    }

    public void IncrementVersion()
    {
        Version++;
    }

    // Strongly-typed domain event addition
    protected void AddDomainEvent<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        base.AddDomainEvent(domainEvent);
    }
}

// Convenience class for long ID aggregates
public abstract class AggregateRoot : AggregateRoot<long>
{
    protected AggregateRoot(long id) : base(id)
    {
    }
}