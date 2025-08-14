using BaseSource.Core.Application.Providers;
using BaseSource.EndPoint.WebApi.Common.Models;
using BaseSource.EndPoint.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Data.Common;

namespace BaseSource.EndPoint.WebApi.Common.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : Controller
{
    protected ProviderFactory Factory => HttpContext.ApplicationContext();

    protected BaseController()
    {
    }

    public override OkObjectResult Ok([ActionResultObjectValue] object? value)
    {
        return base.Ok(ApiResult.Success(value));
    }
    public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object? error)
    {
        return base.BadRequest(ApiResult.Faild(error));
    }

}

[Authorize]
public abstract class AuthorizeController : BaseController
{
    protected AuthorizeController()
    {
    }

}
