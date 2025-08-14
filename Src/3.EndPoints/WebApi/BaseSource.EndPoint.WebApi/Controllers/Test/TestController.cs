using BaseSource.Core.Application.Providers;
using BaseSource.EndPoint.WebApi.Common.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace BaseSource.EndPoint.WebApi.Controllers.Test;

public class TestController : BaseController
{

    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Test endpoint is working!");
    }
}

public class ModelTestJson
{
    public string Name { get; set; }
    public string Family { get; set; }
    public DateTime EndDate { get; set; }
}
