namespace BaseSource.Core.Domain.Common.Aggregate;

// Interface for auditable entities
public interface IAuditableEntity<TId> : IEntity<TId> where TId : notnull
{
    DateTime CreatedAt { get; }
    TId CreatedBy { get; }
    DateTime? LastModifiedAt { get; }
    TId? LastModifiedBy { get; }
}
