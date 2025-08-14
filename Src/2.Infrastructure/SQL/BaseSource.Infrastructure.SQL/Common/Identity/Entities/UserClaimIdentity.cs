namespace BaseSource.Infrastructure.SQL.Common.Identity.Entities;

[Table("UserClaims", Schema = "Identity")]
public class UserClaimIdentity : IdentityUserClaim<long>, IAuditableEntity<int>
{
    public EntityId EntityId { get; protected set; } = EntityId.FromGuid(Guid.NewGuid());
    public bool IsDeleted { get; private set; }
    public bool IsActive { get; private set; }
    public void Delete()
    {
        IsActive = false;
        IsDeleted = true;
    }

    public void Restore()
    {
        IsActive = true;
        IsDeleted = false;
    }
}
