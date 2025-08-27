namespace BaseSource.EndPoint.WebApi.Models;

public class AuthenticationModel
{
    public record LoginModel(
        string UserName,
        string Password,
        bool IsRemember,
        string ReturnUrl
        );
    public record LoginAsModel(string Username);
    public record LoginAsKeyModel(Guid UserEntityId);
    public record LoginAsIdModel(long UserId);
}
