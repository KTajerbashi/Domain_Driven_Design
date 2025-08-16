using BaseSource.Core.Application.Interfaces;
using System.Security.Claims;

namespace BaseSource.EndPoint.WebApi.Providers.Identity.Services;

public class CurrentUserServiceFactory : ICurrentUserServiceFactory
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserServiceFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public IUser GetCurrentUser()
    {
        return new CurrentUserService(_httpContextAccessor);
    }

    public ClaimsPrincipal? GetCurrentClaimsPrincipal()
    {
        return _httpContextAccessor.HttpContext?.User;
    }
}
