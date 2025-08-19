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
                if (exceptionHandlerFeature != null)
                {
                    var exception = exceptionHandlerFeature.Error;

                    // Get controller/action/parameters if available
                    var descriptor = context.GetControllerAction();
                    string controller = descriptor.Controller ?? "N/A";
                    string action = descriptor.Action ?? "N/A";
                    string parameters = string.Empty;

                    var httpMethod = context.Request.Method;

                    if (httpMethod == HttpMethods.Get || httpMethod == HttpMethods.Delete)
                    {
                        parameters = context.Request.QueryString.HasValue
                            ? context.Request.QueryString.Value!
                            : "";
                    }
                    else if (httpMethod == HttpMethods.Post || httpMethod == HttpMethods.Put || httpMethod == HttpMethods.Patch)
                    {
                        parameters = await ReadRequestBodyAsync(context);
                    }

                    string userId = context.User?.Identity?.IsAuthenticated == true
                        ? context.User.Identity?.Name ?? "Unknown"
                        : "Anonymous";

                    string userIp = context.Connection.RemoteIpAddress?.ToString() ?? "N/A";

                    // Log exception with all context
                    using (LogContext.PushProperty("Controller", controller))
                    using (LogContext.PushProperty("Action", action))
                    using (LogContext.PushProperty("Parameters", parameters))
                    using (LogContext.PushProperty("UserId", userId))
                    using (LogContext.PushProperty("UserIp", userIp))
                    using (LogContext.PushProperty("HttpMethod", httpMethod))
                    using (LogContext.PushProperty("StatusCode", 500))
                    {
                        Log.Error(exception,
                            "An unhandled exception occurred in {Controller}/{Action} | Parameters: {Parameters} | UserId: {UserId} | UserIp: {UserIp} | HttpMethod: {HttpMethod} | ExceptionMessage: {ExceptionMessage} | StackTrace: {StackTrace}",
                            controller,
                            action,
                            parameters,
                            userId,
                            userIp,
                            httpMethod,
                            exception.Message,
                            exception.StackTrace);
                    }
                    var reqRes = exception.GetExceptionMessage();
                    var response = new
                    {
                        StatusCode = (int)(HttpStatusCode)(reqRes.StatusCode),
                        Message = reqRes.Message,
                    };

                    context.Response.StatusCode = reqRes.StatusCode;
                    await context.Response.WriteAsJsonAsync(response);
                }
            });
        });

        return app;
    }



    private static async Task<string> ReadRequestBodyAsync(HttpContext context)
    {
        context.Request.EnableBuffering();

        using var reader = new StreamReader(
            context.Request.Body,
            Encoding.UTF8,
            detectEncodingFromByteOrderMarks: false,
            bufferSize: 1024,
            leaveOpen: true);

        string body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0; // rewind
        return body;
    }
}
