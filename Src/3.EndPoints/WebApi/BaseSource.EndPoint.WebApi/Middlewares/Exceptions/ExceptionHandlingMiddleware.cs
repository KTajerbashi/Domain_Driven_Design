using BaseSource.Core.Application.Exceptions;
using BaseSource.Core.Domain.Exceptions;
using BaseSource.EndPoint.WebApi.Exceptions;
using BaseSource.Infrastructure.SQL.Command.Exceptions;
using BaseSource.Infrastructure.SQL.Query.Exceptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.Json;

namespace BaseSource.EndPoint.WebApi.Middlewares.Exceptions;

public static class ExceptionHandler
{
    private const string DefaultErrorMessage = "An unexpected error occurred.";
    private const string ValidationErrorMessage = "Validation failed";
    private const string NotFoundMessage = "The requested resource was not found.";
    private const string UnauthorizedMessage = "Unauthorized access.";

    public static WebApplication UseApiExceptionHandler(this WebApplication app)
    {
        app.UseExceptionHandler(new ExceptionHandlerOptions
        {
            AllowStatusCode404Response = true,
            ExceptionHandler = HandleException
        });

        return app;
    }

    private static async Task HandleException(HttpContext context)
    {
        context.Response.ContentType = "application/json";

        var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        if (exceptionHandlerFeature?.Error == null)
        {
            await WriteDefaultErrorResponse(context);
            return;
        }

        var exception = exceptionHandlerFeature.Error;
        var exceptionId = context.TraceIdentifier ?? Guid.NewGuid().ToString();

        try
        {
            // Gather request context
            var (controller, action) = context.GetControllerAction();
            var parameters = await GetRequestParametersAsync(context);
            var userId = GetUserId(context);
            var userIp = context.Connection.RemoteIpAddress?.ToString() ?? "N/A";
            var httpMethod = context.Request.Method;

            // Map exception to response
            var (statusCode, message, details) = MapException(exception);

            // Log with enriched context
            LogException(exception, statusCode, message, controller, action, parameters,
                        userId, userIp, httpMethod, exceptionId);

            // Build response
            var response = BuildErrorResponse(message, details, statusCode, exceptionId,
                                            context.Request.Path, includeDetails: true);

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(response,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
        catch (Exception ex)
        {
            // Fallback error handling if the main handler fails
            Log.Fatal(ex, "Critical failure in exception handler for exception {ExceptionId}", exceptionId);
            await WriteDefaultErrorResponse(context, exceptionId);
        }
    }

    private static (int StatusCode, string Message, string? Details) MapException(Exception exception)
    {
        return exception switch
        {
            ValidationException validationException => (
                (int)HttpStatusCode.BadRequest,
                ValidationErrorMessage,
                FormatValidationErrors(validationException.Errors)
            ),

            DomainException domainEx => (
                (int)HttpStatusCode.BadRequest,
                domainEx.Message,
                exception.Message
            ),

            AppException appEx => (
                (int)HttpStatusCode.BadRequest,
                appEx.Message,
                exception.Message
            ),

            InfrastructureCommandException commandEx => (
                (int)HttpStatusCode.BadRequest,
                commandEx.Message,
                commandEx.InnerException?.Message
            ),

            InfrastructureQueryException queryEx => (
                (int)HttpStatusCode.BadRequest,
                queryEx.Message,
                queryEx.InnerException?.Message
            ),

            WebApiException apiEx => (
                (int)HttpStatusCode.BadRequest,
                apiEx.Message,
                exception.Message
            ),

            KeyNotFoundException => (
                (int)HttpStatusCode.NotFound,
                NotFoundMessage,
                null
            ),

            UnauthorizedAccessException => (
                (int)HttpStatusCode.Unauthorized,
                UnauthorizedMessage,
                null
            ),

            OperationCanceledException => (
                (int)HttpStatusCode.ServiceUnavailable,
                "Request was cancelled.",
                null
            ),

            TimeoutException => (
                (int)HttpStatusCode.RequestTimeout,
                "Request timed out.",
                null
            ),

            _ => (
                (int)HttpStatusCode.InternalServerError,
                DefaultErrorMessage,
                null
            )
        };
    }

    private static string FormatValidationErrors(IEnumerable<FluentValidation.Results.ValidationFailure> errors)
    {
        return string.Join("; ", errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"));
    }

    private static void LogException(Exception exception, int statusCode, string message,
                                   string controller, string action, string parameters,
                                   string userId, string userIp, string httpMethod,
                                   string exceptionId)
    {
        var logLevel = statusCode >= 500 ? LogEventLevel.Error : LogEventLevel.Warning;

        using (LogContext.PushProperty("ExceptionId", exceptionId))
        using (LogContext.PushProperty("Controller", controller))
        using (LogContext.PushProperty("Action", action))
        using (LogContext.PushProperty("Parameters", parameters))
        using (LogContext.PushProperty("UserId", userId))
        using (LogContext.PushProperty("UserIp", userIp))
        using (LogContext.PushProperty("HttpMethod", httpMethod))
        using (LogContext.PushProperty("StatusCode", statusCode))
        using (LogContext.PushProperty("ErrorMessage", message))
        {
            if (statusCode >= 500)
            {
                Log.ForContext("ExceptionType", exception.GetType().Name)
                   .Error(exception,
                       "Unhandled exception {ExceptionId} in {Controller}/{Action} | Status: {StatusCode}",
                       exceptionId, controller, action, statusCode);
            }
            else
            {
                Log.ForContext("ExceptionType", exception.GetType().Name)
                   .Warning(exception,
                       "Client error {ExceptionId} in {Controller}/{Action} | Status: {StatusCode} | Message: {ErrorMessage}",
                       exceptionId, controller, action, statusCode, message);
            }
        }
    }

    private static object BuildErrorResponse(string message, string? details, int statusCode,
                                           string exceptionId, string path, bool includeDetails = false)
    {
        var response = new
        {
            Type = GetErrorType(statusCode),
            Title = message,
            Status = statusCode,
            Detail = includeDetails ? details : null,
            Instance = path,
            TraceId = exceptionId,
            Timestamp = DateTime.UtcNow
        };

        return response;
    }

    private static string GetErrorType(int statusCode)
    {
        return statusCode switch
        {
            (int)HttpStatusCode.BadRequest => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            (int)HttpStatusCode.Unauthorized => "https://tools.ietf.org/html/rfc7235#section-3.1",
            (int)HttpStatusCode.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            (int)HttpStatusCode.InternalServerError => "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            _ => "https://tools.ietf.org/html/rfc7231#section-6.5.1"
        };
    }

    private static async Task<string> GetRequestParametersAsync(HttpContext context)
    {
        try
        {
            var httpMethod = context.Request.Method;

            if (httpMethod == HttpMethods.Get || httpMethod == HttpMethods.Delete)
            {
                return context.Request.QueryString.HasValue ? context.Request.QueryString.Value! : "";
            }

            if (httpMethod == HttpMethods.Post || httpMethod == HttpMethods.Put || httpMethod == HttpMethods.Patch)
            {
                context.Request.EnableBuffering();

                using var reader = new StreamReader(
                    context.Request.Body,
                    Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    bufferSize: 4096, // Increased buffer size for better performance
                    leaveOpen: true);

                var body = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;

                // Truncate very large request bodies to prevent log flooding
                return body.Length > 1000 ? body[..1000] + "..." : body;
            }

            return "";
        }
        catch (Exception ex)
        {
            Log.Warning(ex, "Failed to read request parameters");
            return "Error reading parameters";
        }
    }

    private static string GetUserId(HttpContext context)
    {
        if (context.User?.Identity?.IsAuthenticated != true)
            return "Anonymous";

        return context.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ??
               context.User.Identity.Name ??
               "Authenticated";
    }

    private static async Task WriteDefaultErrorResponse(HttpContext context, string? exceptionId = null)
    {
        var response = new
        {
            Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
            Title = DefaultErrorMessage,
            Status = (int)HttpStatusCode.InternalServerError,
            TraceId = exceptionId ?? Guid.NewGuid().ToString(),
            Timestamp = DateTime.UtcNow
        };

        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        await context.Response.WriteAsJsonAsync(response);
    }
}

// Extension method for controller/action detection
public static class HttpContextExtensions
{
    public static (string? Controller, string? Action) GetControllerAction(this HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        if (endpoint?.Metadata.GetMetadata<ControllerActionDescriptor>() is ControllerActionDescriptor descriptor)
        {
            return (descriptor.ControllerName, descriptor.ActionName);
        }

        return ("Unknown", "Unknown");
    }
}