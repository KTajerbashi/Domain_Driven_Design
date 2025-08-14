using BaseSource.EndPoint.WebApi.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.EndPoint.WebApi.Controllers.Test;

public class AuthTestController : AuthorizeController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Test endpoint is working!");
    }
}