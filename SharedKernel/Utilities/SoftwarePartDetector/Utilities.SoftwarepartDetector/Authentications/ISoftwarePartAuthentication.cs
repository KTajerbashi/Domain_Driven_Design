using IdentityModel.Client;

namespace Utilities.SoftwarepartDetector.Authentications;

public interface ISoftwarePartAuthentication
{
    Task<TokenResponse> LoginAsync();
}