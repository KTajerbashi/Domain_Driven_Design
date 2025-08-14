namespace BaseSource.EndPoint.WebApi.Providers.Identity.Options;

public class IdentityOption
{
    public JwtOption Jwt { get; set; }
}
public class JwtOption
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpireMinutes { get; set; } = 30;
    public int RefreshTokenExpireDays { get; set; } = 7;
}