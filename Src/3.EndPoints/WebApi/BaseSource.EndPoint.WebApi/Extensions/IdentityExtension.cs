using System.Security.Claims;

namespace BaseSource.EndPoint.WebApi.Extensions;

public static class IdentityExtension
{
    public static string GetStringClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        if (claimsPrincipal == null) return string.Empty;

        var claim = claimsPrincipal.FindFirst(claimType);
        return claim?.Value ?? string.Empty;
    }

    public static long GetLongClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        var value = claimsPrincipal.GetStringClaim(claimType);
        return long.TryParse(value, out var result) ? result : 0;
    }

    public static bool GetBoolClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        var value = claimsPrincipal.GetStringClaim(claimType);
        return bool.TryParse(value, out var result) && result;
    }

    public static IEnumerable<string> GetArrayClaim(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        var value = claimsPrincipal.GetStringClaim(claimType);
        return string.IsNullOrEmpty(value)
            ? Enumerable.Empty<string>()
            : value.Split(',', StringSplitOptions.RemoveEmptyEntries);
    }
}

