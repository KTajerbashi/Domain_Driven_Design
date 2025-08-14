namespace BaseSource.Infrastructure.SQL.Common.Identity.Entities;

[Table("UserLogins", Schema = "Identity")]
public class UserLoginIdentity : IdentityUserLogin<long>, IAuditableEntity<long>
{
    public long Id { get; private set; }
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
