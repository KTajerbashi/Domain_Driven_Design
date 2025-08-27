namespace BaseSource.Infrastructure.SQL.Command.Repositories.Auth.Interfaces;

public class JwtSettings
{
    public string Secret { get; set; } = string.Empty;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int TokenExpirationMinutes { get; set; } = 60;
    public int RefreshTokenExpirationDays { get; set; } = 7;
    public int ClockSkewMinutes { get; set; } = 5;

    // Helper properties
    public TimeSpan TokenExpiration => TimeSpan.FromMinutes(TokenExpirationMinutes);
    public TimeSpan RefreshTokenExpiration => TimeSpan.FromDays(RefreshTokenExpirationDays);
    public TimeSpan ClockSkew => TimeSpan.FromMinutes(ClockSkewMinutes);
}
public class JweSettings
{
    public string EncryptionKey { get; set; } = string.Empty;
    public string EncryptionAlgorithm { get; set; } = "A256GCM";
    public string KeyManagementAlgorithm { get; set; } = "dir";
    public int TokenExpirationMinutes { get; set; } = 60;
    public string CompressionAlgorithm { get; set; } = "DEF";

    // Helper properties
    public TimeSpan TokenExpiration => TimeSpan.FromMinutes(TokenExpirationMinutes);
}

public class IdentityOptionsSettings
{
    public PasswordSettings Password { get; set; } = new();
    public LockoutSettings Lockout { get; set; } = new();
    public UserSettings User { get; set; } = new();
    public SignInSettings SignIn { get; set; } = new();
}

public class PasswordSettings
{
    public int RequiredLength { get; set; } = 6;
    public int RequiredUniqueChars { get; set; } = 1;
    public bool RequireNonAlphanumeric { get; set; } = true;
    public bool RequireLowercase { get; set; } = true;
    public bool RequireUppercase { get; set; } = true;
    public bool RequireDigit { get; set; } = true;
}

public class LockoutSettings
{
    public TimeSpan DefaultLockoutTimeSpan { get; set; } = TimeSpan.FromMinutes(30);
    public int MaxFailedAccessAttempts { get; set; } = 5;
    public bool AllowedForNewUsers { get; set; } = true;
}

public class UserSettings
{
    public bool RequireUniqueEmail { get; set; } = true;
    public string AllowedUserNameCharacters { get; set; } = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
}

public class SignInSettings
{
    public bool RequireConfirmedEmail { get; set; } = false;
    public bool RequireConfirmedPhoneNumber { get; set; } = false;
}