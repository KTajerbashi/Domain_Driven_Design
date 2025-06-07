using Microsoft.AspNetCore.Mvc;

namespace BaseSource.EndPoint.WebApi.Common.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : Controller
{
    protected BaseController()
    {
    }
    
}
