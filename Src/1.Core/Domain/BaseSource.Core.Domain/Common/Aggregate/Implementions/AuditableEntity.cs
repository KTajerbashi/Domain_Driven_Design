using BaseSource.Core.Domain.Common.Aggregate.Interfaces;

namespace BaseSource.Core.Domain.Common.Aggregate.Implementions;

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
