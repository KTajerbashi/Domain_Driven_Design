using BaseSource.Core.Application.Common.Handlers.Command;
using BaseSource.Core.Application.Common.Handlers.Query;
using BaseSource.Core.Application.Providers;
using BaseSource.EndPoint.WebApi.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BaseSource.EndPoint.WebApi.Common.Controllers;

[ApiController]
[Route("api/[controller]")]
public abstract class BaseController : Controller
{
    protected ProviderFactory Factory => HttpContext.ApplicationContext();

    protected BaseController()
    {
    }

    protected virtual async Task<IActionResult> CreateAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse> => Ok(await Factory.Mediator.Send(command));
    protected virtual async Task<IActionResult> UpdateAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse> => Ok(await Factory.Mediator.Send(command));
    protected virtual async Task<IActionResult> DeleteAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse> => Ok(await Factory.Mediator.Send(command));
    protected virtual async Task<IActionResult> GetByIdAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse> => Ok(await Factory.Mediator.Send(query));
    protected virtual async Task<IActionResult> GetAllAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse> => Ok(await Factory.Mediator.Send(query));

    protected virtual async Task<IActionResult> CommandAsync<TCommand, TResponse>(TCommand command) where TCommand : ICommand<TResponse> => Ok(await Factory.Mediator.Send(command));
    protected virtual async Task<IActionResult> CommandAsync<TCommand>(TCommand command)
        where TCommand : ICommand
    {
        await Factory.Mediator.Send(command);
        return base.Ok(ApiResult.Success("Success"));
    }


    protected virtual async Task<IActionResult> QueryAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse> => Ok(await Factory.Mediator.Send(query));
    protected virtual async Task<IActionResult> QueryListAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<List<TResponse>> => Ok(await Factory.Mediator.Send(query));

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
