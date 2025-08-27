namespace BaseSource.Infrastructure.SQL.Command.Repositories.Auth.Interfaces;

// Extension methods for result objects
public static class IdentityResultExtensions
{
    public static AuthenticationResult Failed(string error)
    {
        return new AuthenticationResult(false, null, null, null, new[] { error });
    }

    public static AuthenticationResult Failed(IEnumerable<string> errors)
    {
        return new AuthenticationResult(false, null, null, null, errors);
    }

    public static TokenValidationResult FailedTokenValidation(string error)
    {
        return new TokenValidationResult(false, null, new[] { error });
    }

    public static TokenValidationResult FailedTokenValidation(IEnumerable<string> errors)
    {
        return new TokenValidationResult(false, null, errors);
    }
}


