using BaseSource.Core.Application.Providers.Autofac;

namespace BaseSource.Core.Application.Interfaces;

public interface IUser : IAutofacScopedLifetime
{
    string Name { get; }
    string Family { get; }
    string DisplayName { get; }
    long UserId { get; }
    long UserRoleId { get; }
    string RoleName { get; }
    string RoleTitle { get; }
    string Ip { get; }
    string Agent { get; }
    string Username { get; }
    string Email { get; }
}

