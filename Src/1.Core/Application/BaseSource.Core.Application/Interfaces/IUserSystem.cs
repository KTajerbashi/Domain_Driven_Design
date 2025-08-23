using BaseSource.Core.Application.Providers.Autofac;
using System.Security.Claims;

namespace BaseSource.Core.Application.Interfaces;


public interface ICurrentUserServiceFactory : IScopedLifetime
{
    IUserSystem GetCurrentUser();
    ClaimsPrincipal? GetCurrentClaimsPrincipal();
}


public interface IUserSystem : IScopedLifetime
{
    // Basic user information
    string Name { get; }
    string Family { get; }
    string DisplayName { get; }
    long UserId { get; }
    string Username { get; }
    string Email { get; }

    // Role information
    long UserRoleId { get; }
    string RoleName { get; }
    string RoleTitle { get; }

    // Authentication status
    bool IsAuthenticated { get; }

    // Additional contact information
    string PhoneNumber { get; }
    bool EmailVerified { get; }

    // Authorization information
    IEnumerable<string> Permissions { get; }
    string TenantId { get; }

    // Request context
    string Ip { get; }
    string Agent { get; }

    // Claim access methods
    string GetClaim(string claimType);
    bool IsInRole(string roleName);
    bool HasPermission(string permission);

    // Optional: Provide access to the full ClaimsPrincipal
    ClaimsPrincipal? GetClaimsPrincipal();
}