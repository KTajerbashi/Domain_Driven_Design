using BaseSource.Core.Domain.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using Serilog.Context;
using System.Net;

namespace BaseSource.EndPoint.WebApi.Middlewares.Exceptions;

public static class ExceptionHandler
{
    public static WebApplication UseApiExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(builder =>
        {
            builder.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (exceptionHandlerFeature is null)
                    return;

                var exception = exceptionHandlerFeature.Error;

                // --- Gather request context ---
                var descriptor = context.GetControllerAction();
                string controller = descriptor.Controller ?? "N/A";
                string action = descriptor.Action ?? "N/A";
                string parameters = await GetRequestParametersAsync(context);

                string userId = context.User?.Identity?.IsAuthenticated == true
                    ? context.User.Identity?.Name ?? "Unknown"
                    : "Anonymous";

                string userIp = context.Connection.RemoteIpAddress?.ToString() ?? "N/A";
                string httpMethod = context.Request.Method;

                // --- Map exception to response ---
                var (statusCode, message, details) = MapException(exception);

                // --- Logging with Serilog & context ---
                using (LogContext.PushProperty("Controller", controller))
                using (LogContext.PushProperty("Action", action))
                using (LogContext.PushProperty("Parameters", parameters))
                using (LogContext.PushProperty("Message", message))
                using (LogContext.PushProperty("UserId", userId))
                using (LogContext.PushProperty("UserIp", userIp))
                using (LogContext.PushProperty("HttpMethod", httpMethod))
                using (LogContext.PushProperty("StatusCode", statusCode))
                {
                    Log.Error(exception,
                        "Unhandled exception in {Controller}/{Action} | Params: {Parameters} | User: {UserId} | IP: {UserIp} | Method: {HttpMethod}",
                        controller, action, parameters, userId, userIp, httpMethod);
                }

                // --- Build consistent JSON response ---
                var response = new
                {
                    error = message,
                    details,
                    statusCode,
                    timestamp = DateTime.UtcNow
                };

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(response);
            });
        });

        return app;
    }

    private static (int StatusCode, string Message, string? Details) MapException(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => (
                (int)HttpStatusCode.BadRequest,
                "Validation failed",
                string.Join("; ", validationException.Errors.Select(e => e.ErrorMessage))
            ),

            DomainException domainException => (
                (int)HttpStatusCode.BadRequest,
                domainException.Message,
                null
            ),

            KeyNotFoundException => (
                (int)HttpStatusCode.NotFound,
                "The requested resource was not found.",
                null
            ),

            UnauthorizedAccessException => (
                (int)HttpStatusCode.Unauthorized,
                "Unauthorized access.",
                null
            ),

            _ => (
                (int)HttpStatusCode.InternalServerError,
                "An unexpected error occurred.",
                null
            )
        };
    }

    private static async Task<string> GetRequestParametersAsync(HttpContext context)
    {
        var httpMethod = context.Request.Method;

        if (httpMethod == HttpMethods.Get || httpMethod == HttpMethods.Delete)
        {
            return context.Request.QueryString.HasValue
                ? context.Request.QueryString.Value!
                : "";
        }

        if (httpMethod == HttpMethods.Post || httpMethod == HttpMethods.Put || httpMethod == HttpMethods.Patch)
        {
            context.Request.EnableBuffering();

            using var reader = new StreamReader(
                context.Request.Body,
                Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1024,
                leaveOpen: true);

            string body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
            return body;
        }

        return "";
    }
}
