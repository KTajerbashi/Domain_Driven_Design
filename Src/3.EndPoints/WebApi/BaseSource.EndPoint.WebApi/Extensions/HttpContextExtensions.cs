using BaseSource.Core.Application.Exceptions;
using BaseSource.Core.Application.Providers;
using BaseSource.Core.Domain.Exceptions;
using BaseSource.EndPoint.WebApi.Exceptions;
using BaseSource.Infrastructure.SQL.Command.Exceptions;
using BaseSource.Infrastructure.SQL.Query.Exceptions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Security.Claims;

namespace BaseSource.EndPoint.WebApi.Extensions;

public static class ExceptionExtensions
{
    /// <summary>
    /// Returns a user-friendly message for known exception types, or a generic message for unknown exceptions.
    /// </summary>
    public static (int StatusCode,string Message) GetExceptionMessage(this Exception exception)
    {
        if (exception == null) return (500, "An unexpected error occurred.");

        var message = exception switch
        {
            DomainException domainEx => domainEx.Message,   // domain validation or business rules
            AppException appEx => appEx.Message,           // application-specific errors
            InfrastructureCommandException cmdEx => cmdEx.Message, // errors from commands
            InfrastructureQueryException queryEx => queryEx.Message, // errors from queries
            WebApiException apiEx => apiEx.Message,       // API-specific errors
            _ => "An unexpected error occurred. Please try again later." // unknown exception
        };

        var statusCode = exception switch
        {
            DomainException or   // domain validation or business rules
            AppException or // application-specific errors
            InfrastructureCommandException or // errors from commands
            InfrastructureQueryException or // errors from queries
            WebApiException => 400,       // API-specific errors
            _ => 500 // unknown exception
        };

        return (statusCode, message);
    }
}



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
