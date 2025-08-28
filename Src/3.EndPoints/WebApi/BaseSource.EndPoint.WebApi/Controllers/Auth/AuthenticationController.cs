using BaseSource.Infrastructure.SQL.Command.Repositories.Auth.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        try
        {
            var result = await _identityFactory.LoginAsync(parameter.UserName, parameter.Password, parameter.IsRemember);
            return Ok(result);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpGet("LoginAs/{username}")]
    public async Task<IActionResult> LoginAs(string username)
    {
        try
        {
            LoginAsModel parameter = new(username);
            var entity = await _identityFactory.UserManager.FindByNameAsync(username) ?? await _identityFactory.UserManager.FindByEmailAsync(username);

            if (entity is null)
                return NotFound($"User not found with : {username}");

            var result = await _identityFactory.LoginAsAsync(entity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpGet("Signout")]
    public async Task<IActionResult> Signout()
    {
        try
        {
            await _identityFactory.SignOutAsync(Factory.User.UserId);
            return Ok();
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    [HttpGet("LoginAsKey/{key}")]
    public async Task<IActionResult> LoginAsKey(Guid key)
    {
        try
        {
            LoginAsKeyModel parameter = new(key);
            var entity = await _identityFactory.UserManager.Users.Where(item => item.EntityId.Equals(key)).FirstOrDefaultAsync();
            if (entity is null) return NotFound($"User not found with : {key}");

            var result = await _identityFactory.LoginAsAsync(entity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpGet("LoginAsIdentity/{id}")]
    public async Task<IActionResult> LoginAsIdentity(long id)
    {
        try
        {
            LoginAsIdModel parameter = new(id);
            var entity = await _identityFactory.UserManager.FindByNameAsync($"{id}");
            if (entity is null) return NotFound($"User not found with : {id}");
            var result = await _identityFactory.LoginAsAsync(entity);
            return Ok(result);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [HttpGet("Authenticated")]
    public async Task<IActionResult> Authenticated()
    {
        return Ok(User.Identity.IsAuthenticated);
    }

    [HttpGet("Users")]
    public async Task<IActionResult> Users()
    {
        return Ok(await _identityFactory.UserManager.Users.ToListAsync());
    }
}
