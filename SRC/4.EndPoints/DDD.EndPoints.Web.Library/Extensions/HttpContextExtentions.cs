using DDD.Core.Contracts.Library.ApplicationServices.Commands;
using DDD.Core.Contracts.Library.ApplicationServices.Events;
using DDD.Core.Contracts.Library.ApplicationServices.Queries;
using DDD.Utilities.Library;
using Microsoft.AspNetCore.Http;

namespace DDD.EndPoints.Web.Library.Extensions;

/// <summary>
/// 
/// </summary>
public static class HttpContextExtensions
{
    public static ICommandDispatcher CommandDispatcher(this HttpContext httpContext) =>
        (ICommandDispatcher)httpContext.RequestServices.GetService(typeof(ICommandDispatcher));

    public static IQueryDispatcher QueryDispatcher(this HttpContext httpContext) =>
        (IQueryDispatcher)httpContext.RequestServices.GetService(typeof(IQueryDispatcher));
    public static IEventDispatcher EventDispatcher(this HttpContext httpContext) =>
        (IEventDispatcher)httpContext.RequestServices.GetService(typeof(IEventDispatcher));
    public static UtilitiesServices ApplicationContext(this HttpContext httpContext) =>
        (UtilitiesServices)httpContext.RequestServices.GetService(typeof(UtilitiesServices));
}
