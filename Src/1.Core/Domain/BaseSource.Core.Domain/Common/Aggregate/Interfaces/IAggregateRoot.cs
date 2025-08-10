namespace BaseSource.Core.Domain.Common.Aggregate.Interfaces;

// Interface for aggregate roots
public interface IAggregateRoot<TId> : IAuditableEntity<TId> where TId : notnull
{
    int Version { get; }
    void IncrementVersion();
}
