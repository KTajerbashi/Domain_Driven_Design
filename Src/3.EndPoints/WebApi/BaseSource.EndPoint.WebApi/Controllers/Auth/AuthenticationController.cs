using BaseSource.Infrastructure.SQL.Command.Repositories.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static BaseSource.EndPoint.WebApi.Models.AuthenticationModel;

namespace BaseSource.EndPoint.WebApi.Controllers.Auth;

public class AuthenticationController : BaseController
{

    private readonly IIdentityFactory _identityFactory;
    public AuthenticationController(IIdentityFactory identityFactory)
    {
        _identityFactory = identityFactory;
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginModel parameter)
    {
        var result = await _identityFactory.LoginAsync(parameter.UserName,parameter.Password,parameter.IsRemember);
        return Ok(result);
    }

    [HttpGet("LoginAs/{username}")]
    public async Task<IActionResult> LoginAs(string username)
    {
        LoginAsModel parameter = new(username);
        return Ok(parameter);
    }

    [HttpGet("LoginAsKey/{key}")]
    public async Task<IActionResult> LoginAsKey(Guid key)
    {
        LoginAsKeyModel parameter = new(key);
        return Ok(parameter);
    }

    [HttpGet("LoginAsIdentity/{id}")]
    public async Task<IActionResult> LoginAsIdentity(long id)
    {
        LoginAsIdModel parameter = new(id);
        return Ok(parameter);
    }

    [HttpGet("Authenticated")]
    public async Task<IActionResult> Authenticated()
    {
        return Ok(User.Identity.IsAuthenticated);
    }
}
