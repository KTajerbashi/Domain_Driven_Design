using BaseSource.EndPoint.WebApi.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.EndPoint.WebApi.Controllers;

public class TestController : BaseController
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Test endpoint is working!");
    }
}