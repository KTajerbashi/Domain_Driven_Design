using BaseSource.Core.Application.Providers;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Security.Claims;

namespace BaseSource.EndPoint.WebApi.Extensions;



public static class HttpContextExtensions
{
    public static ProviderFactory ApplicationContext(this HttpContext httpContext) =>
        (ProviderFactory)httpContext.RequestServices.GetService(typeof(ProviderFactory))!;

    public static (string? Controller, string? Action) GetControllerAction(this HttpContext context)
    {
        var path = context?.Request?.Path.Value?.Split("/");
        if (path.Length > 0 && path.Length > 3)
            return (path[2], path[3]);
        return (string.Empty, string.Empty);
    }
    public static (string? Controller, string? Action) GetControllerActionNames(this HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint == null) return (null, null);

        var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
        if (actionDescriptor == null) return (null, null);

        return (actionDescriptor.ControllerName, actionDescriptor.ActionName);
    }
    public static string GetUserId(this HttpContext context)
    {
        if (context.User?.Identity?.IsAuthenticated != true)
            return "Anonymous";

        var result = context.User.FindFirst("UserId")?.Value
            ?? context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? context.User.Identity.Name
            ?? "Authenticated";

        return result;
    }

    public static string GetUserIp(this HttpContext context)
    {
        return context.Connection.RemoteIpAddress?.ToString()
            ?? context.Request.Headers["X-Forwarded-For"].FirstOrDefault()
            ?? context.Request.Headers["X-Real-IP"].FirstOrDefault()
            ?? "Unknown";
    }

}
