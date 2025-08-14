using MediatR;

namespace BaseSource.Core.Domain.Common.Event;

// Marker interface for domain events
public interface IDomainEvent : INotification
{
    DateTime OccurredOn { get; }
}

// Base domain event implementation
public abstract record DomainEvent : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}