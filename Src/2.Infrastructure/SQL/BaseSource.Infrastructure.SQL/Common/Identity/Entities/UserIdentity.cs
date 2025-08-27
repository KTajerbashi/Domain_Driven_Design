using BaseSource.Infrastructure.SQL.Common.Identity.Entities.Parameters;

namespace BaseSource.Infrastructure.SQL.Common.Identity.Entities;


[Table("Users", Schema = "Identity")]
public class UserIdentity : IdentityUser<long>, IAuditableEntity<long>
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string NationalCode { get; private set; }
    public bool IsDeleted { get; private set; }
    public bool IsActive { get; private set; }
    public EntityId EntityId { get; protected set; } = EntityId.FromGuid(Guid.NewGuid());
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
    public UserIdentity()
    {

    }
    public UserIdentity(UserCreateParameters parameters)
    {
        UserName = parameters.UserName;
        Email = parameters.Email;
        FirstName = parameters.FirstName;
        LastName = parameters.LastName;
        PhoneNumber = parameters.PhoneNumber;
        NationalCode = parameters.NationalCode;
    }
}
