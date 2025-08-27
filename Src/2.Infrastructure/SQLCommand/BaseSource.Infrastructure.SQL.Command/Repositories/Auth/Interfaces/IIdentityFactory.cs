using System.Security.Claims;

namespace BaseSource.Infrastructure.SQL.Command.Repositories.Auth.Interfaces;

public interface IIdentityFactory : IScopedLifetime
{
    // Authentication
    Task<AuthenticationResult> LoginAsync(string username, string password, bool rememberMe = false);
    Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);
    Task SignOutAsync(long userId);

    // Token Management
    Task<TokenValidationResult> ValidateTokenAsync(string token);
    Task<bool> RevokeTokenAsync(string token, long userId);

    // Token Generation
    Task<string> GenerateJwtTokenAsync(UserIdentity user);
    Task<string> GenerateJweTokenAsync(UserIdentity user);
    Task<string> GenerateRefreshTokenAsync(UserIdentity user);
    Task<string> GenerateJweRefreshTokenAsync(UserIdentity user);

    // Claims Management
    Task<IdentityResult> AddClaimsAsync(long userId, IEnumerable<Claim> claims);
    Task<IdentityResult> RemoveClaimsAsync(long userId, IEnumerable<string> claimTypes);

    // User Token Management
    Task<IdentityResult> AddUserTokenAsync(long userId, string loginProvider, string name, string value);
    Task<IdentityResult> RemoveUserTokenAsync(long userId, string loginProvider, string name);

    // User Login Management
    Task<IdentityResult> AddUserLoginAsync(long userId, string loginProvider, string providerKey);
    Task<IdentityResult> RemoveUserLoginAsync(long userId, string loginProvider, string providerKey);
}



