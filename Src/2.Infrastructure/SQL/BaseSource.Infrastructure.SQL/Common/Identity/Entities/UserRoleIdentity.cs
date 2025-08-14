namespace BaseSource.Infrastructure.SQL.Common.Identity.Entities;

[Table("UserRoles", Schema = "Identity")]
public class UserRoleIdentity : IdentityUserRole<long>, IAuditableEntity<long>
{
    public long Id { get; private set; }
    public EntityId EntityId { get; protected set; } = EntityId.FromGuid(Guid.NewGuid());
    public bool IsDefault { get; private set; }
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
    public UserRoleIdentity()
    {

    }
    public UserRoleIdentity(long userId, long roleId, bool isDefault)
    {
        UserId = userId;
        RoleId = roleId;
        IsDefault = isDefault;
    }
}
