using BaseSource.Core.Application.Interfaces;
using System.Security.Claims;

namespace BaseSource.EndPoint.WebApi.Providers.Identity.Services;
public class CurrentUserService : IUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClaimsPrincipal? _user;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _user = _httpContextAccessor.HttpContext?.User;
    }

    private string GetStringClaim(string claimType)
    {
        if (_user == null) return string.Empty;

        var claim = _user.FindFirst(claimType);
        return claim?.Value ?? string.Empty;
    }

    private long GetLongClaim(string claimType)
    {
        var value = GetStringClaim(claimType);
        return long.TryParse(value, out var result) ? result : 0;
    }

    private bool GetBoolClaim(string claimType)
    {
        var value = GetStringClaim(claimType);
        return bool.TryParse(value, out var result) && result;
    }

    private IEnumerable<string> GetArrayClaim(string claimType)
    {
        var value = GetStringClaim(claimType);
        return string.IsNullOrEmpty(value)
            ? Enumerable.Empty<string>()
            : value.Split(',', StringSplitOptions.RemoveEmptyEntries);
    }

    public string Name => GetStringClaim("Name");
    public string Family => GetStringClaim("Family");
    public string DisplayName => $"{Name} {Family}".Trim();
    public long UserId => GetLongClaim("UserId");
    public string Username => GetStringClaim("Username");
    public string Email => GetStringClaim(ClaimTypes.Email);
    public long UserRoleId => GetLongClaim("UserRoleId");
    public string RoleName => GetStringClaim("RoleName");
    public string RoleTitle => GetStringClaim("RoleTitle");
    public bool IsAuthenticated => _user?.Identity?.IsAuthenticated ?? false;

    // Additional claims that might be useful
    public string PhoneNumber => GetStringClaim(ClaimTypes.MobilePhone);
    public bool EmailVerified => GetBoolClaim("email_verified");
    public IEnumerable<string> Permissions => GetArrayClaim("permissions");
    public string TenantId => GetStringClaim("TenantId");

    public string Ip => _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    public string Agent => _httpContextAccessor.HttpContext?.Request.Headers.UserAgent.ToString() ?? string.Empty;

    // Method to get any custom claim
    public string GetClaim(string claimType) => GetStringClaim(claimType);

    // Method to check if user has a specific role
    public bool IsInRole(string roleName) => _user?.IsInRole(roleName) ?? false;

    // Method to check if user has a specific permission
    public bool HasPermission(string permission) => Permissions.Contains(permission);

    public ClaimsPrincipal? GetClaimsPrincipal() => _user;
}