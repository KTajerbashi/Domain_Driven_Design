using BaseSource.Kernel.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;
using Serilog.Context;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Claims;
using System.Text;

namespace BaseSource.EndPoint.WebApi.Providers.Logger.SerilogProvider;

public static class SerilogExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        builder.Host.UseSerilog();

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<LogActionFilter>();
        });

        return builder;
    }

    public static WebApplication UseSerilog(this WebApplication app)
    {
        app.UseMiddleware<RequestLoggingMiddleware>();
        return app;
    }
}
public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var request = context.Request;

        // Read request body (buffered copy)
        request.EnableBuffering();
        var bodyReader = new StreamReader(request.Body);
        string requestBody = await bodyReader.ReadToEndAsync();
        request.Body.Position = 0;

        // Get route data
        var controller = context.GetRouteValue("controller")?.ToString();
        var action = context.GetRouteValue("action")?.ToString();

        using (LogContext.PushProperty("Controller", controller))
        using (LogContext.PushProperty("Action", action))
        using (LogContext.PushProperty("RequestBody", requestBody))
        {
            _logger.LogInformation("Handled request {Method} {Path}", request.Method, request.Path);
        }

        await _next(context);
    }
}

public class LogActionFilter : IActionFilter
{
    private readonly ILogger<LogActionFilter> _logger;

    public LogActionFilter(ILogger<LogActionFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;

        var controller = context.RouteData.Values["controller"];
        var action = context.RouteData.Values["action"];
        var parameters = JsonSerializer.Serialize(context.ActionArguments);
        // Read request body (buffered copy)
        request.EnableBuffering();
        var bodyReader = new StreamReader(request.Body);
        string requestBody = bodyReader.ReadToEnd();
        request.Body.Position = 0;

        using (LogContext.PushProperty("Controller", controller))
        using (LogContext.PushProperty("Action", action))
        using (LogContext.PushProperty("RequestBody", requestBody))
        {
            _logger.LogInformation("Handled request {Method} {Path}", request.Method, request.Path);
        }
        _logger.LogInformation("Executing Controller={Controller}, Action={Action}, Parameters={Parameters}",
            controller, action, parameters);
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception == null && context.Result != null)
        {
            _logger.LogInformation("Executed Action Result: {Result}", context.Result.ToString());
        }
    }
}
