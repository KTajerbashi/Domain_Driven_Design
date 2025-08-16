using BaseSource.Core.Application.Interfaces;
using BaseSource.EndPoint.WebApi.Extensions;
using System.Security.Claims;

namespace BaseSource.EndPoint.WebApi.Providers.Identity.Services;
public class CurrentUserService : IUserSystem
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ClaimsPrincipal? _user;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _user = _httpContextAccessor.HttpContext?.User;
    }



    public string Name => _user?.GetStringClaim("Name");
    public string Family => _user?.GetStringClaim("Family");
    public string DisplayName => $"{Name} {Family}".Trim();
    public long UserId => _user.GetLongClaim("UserId");
    public string Username => _user?.GetStringClaim("Username");
    public string Email => _user?.GetStringClaim(ClaimTypes.Email);
    public long UserRoleId => _user.GetLongClaim("UserRoleId");
    public string RoleName => _user.GetStringClaim("RoleName");
    public string RoleTitle => _user?.GetStringClaim("RoleTitle");
    public bool IsAuthenticated => _user?.Identity?.IsAuthenticated ?? false;

    // Additional claims that might be useful
    public string PhoneNumber => _user?.GetStringClaim(ClaimTypes.MobilePhone);
    public bool EmailVerified => _user.GetBoolClaim("email_verified");
    public IEnumerable<string> Permissions => _user?.GetArrayClaim("permissions");
    public string TenantId => _user.GetStringClaim("TenantId");

    public string Ip => _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    public string Agent => _httpContextAccessor.HttpContext?.Request.Headers.UserAgent.ToString() ?? string.Empty;

    // Method to get any custom claim
    public string GetClaim(string claimType) => _user?.GetStringClaim(claimType);

    // Method to check if user has a specific role
    public bool IsInRole(string roleName) => _user?.IsInRole(roleName) ?? false;

    // Method to check if user has a specific permission
    public bool HasPermission(string permission) => Permissions.Contains(permission);

    public ClaimsPrincipal? GetClaimsPrincipal() => _user;
}