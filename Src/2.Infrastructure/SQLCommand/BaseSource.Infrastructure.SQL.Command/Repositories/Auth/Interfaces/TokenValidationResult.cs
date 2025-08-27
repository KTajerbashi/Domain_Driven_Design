using System.Security.Claims;

namespace BaseSource.Infrastructure.SQL.Command.Repositories.Auth.Interfaces;

public record AuthenticationResult(bool Succeeded, string? Token, string? RefreshToken, DateTime? Expires, IEnumerable<string> Errors)
{
    public static AuthenticationResult Success(string token, string refreshToken, DateTime expires)
    {
        return new AuthenticationResult(true, token, refreshToken, expires, Enumerable.Empty<string>());
    }

    public static AuthenticationResult Failed(string error)
    {
        return new AuthenticationResult(false, null, null, null, new[] { error });
    }

    public static AuthenticationResult Failed(IEnumerable<string> errors)
    {
        return new AuthenticationResult(false, null, null, null, errors);
    }
}

public record TokenValidationResult(bool IsValid, ClaimsPrincipal? Principal, IEnumerable<string> Errors)
{
    public static TokenValidationResult Success(ClaimsPrincipal principal)
    {
        return new TokenValidationResult(true, principal, Enumerable.Empty<string>());
    }

    public static TokenValidationResult Failed(string error)
    {
        return new TokenValidationResult(false, null, new[] { error });
    }

    public static TokenValidationResult Failed(IEnumerable<string> errors)
    {
        return new TokenValidationResult(false, null, errors);
    }
}

