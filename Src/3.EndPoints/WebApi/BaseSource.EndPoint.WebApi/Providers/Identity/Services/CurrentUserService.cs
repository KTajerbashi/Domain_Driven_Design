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
        => _user?.FindFirstValue(claimType) ?? string.Empty;

    private long GetLongClaim(string claimType)
    {
        var value = _user?.FindFirstValue(claimType);
        return long.TryParse(value, out var result) ? result : 0;
    }

    //public string Name => GetStringClaim(ClaimTypes.GivenName);
    public string Name => GetStringClaim("Name");

    //public string Family => GetStringClaim(ClaimTypes.Surname);
    public string Family => GetStringClaim("Family");

    public string DisplayName => $"{Name} {Family}".Trim();

    public long UserId => GetLongClaim("UserId");
    public string Username => GetStringClaim("Username");
    public string Email => GetStringClaim(ClaimTypes.Email);

    public long UserRoleId => GetLongClaim("UserRoleId"); // Adjust based on your custom claim
    public string RoleName => GetStringClaim("RoleName"); // Use custom role claim if needed
    public string RoleTitle => GetStringClaim("RoleTitle");   // Custom claim for title

    public string Ip => _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
    public string Agent => _httpContextAccessor.HttpContext?.Request.Headers["User-Agent"].ToString() ?? string.Empty;
}
