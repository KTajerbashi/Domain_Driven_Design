using BaseSource.Kernel.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
using Serilog.Context;
using System.Diagnostics;

namespace BaseSource.EndPoint.WebApi.Providers.Logger.SerilogProvider;

public static class SerilogExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        builder.Services.AddLogging(); // ILogger is usually registered by default

        // Read configuration from appsettings.json
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)  // reads Serilog section
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .CreateLogger();

        builder.Host.UseSerilog(Log.Logger, dispose: true);
        return builder;
    }

    public static WebApplication UseSerilog(this WebApplication app)
    {

        app.UseMiddleware<SerilogEnrichmentMiddleware>();
        //app.UseSerilogRequestLogging(opts =>
        //{
        //    opts.MessageTemplate =
        //        "HTTP {RequestMethod} {RequestPath} ({Controller}/{Action}) responded {StatusCode} in {Elapsed:0.0000} ms | Duration: {DurationMs} ms | RequestId: {RequestId}";
        //});

        //app.UseSerilogRequestLogging(opts =>
        //{
        //    opts.MessageTemplate =
        //        "HTTP {RequestMethod} {RequestPath} ({Controller}/{Action}) responded {StatusCode} in {Elapsed:0.0000} ms (RequestId: {RequestId})";
        //});

        app.UseSerilogRequestLogging();

        return app;
    }
}

public class SerilogEnrichmentMiddleware
{
    private readonly RequestDelegate _next;

    public SerilogEnrichmentMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        var requestId = context.TraceIdentifier ?? Guid.NewGuid().ToString();
        var descriptor = context.GetControllerActionNames();

        string controller = descriptor.Controller ?? "N/A";
        string action = descriptor.Action ?? "N/A";
        string httpMethod = context.Request.Method;
        string parameters = string.Empty;

        // ✅ Capture parameters by HTTP method
        if (httpMethod == HttpMethods.Get || httpMethod == HttpMethods.Delete)
        {
            parameters = context.Request.QueryString.HasValue
                ? context.Request.QueryString.Value!
                : context.Request.RouteValues.ToJson();
        }
        else if (httpMethod == HttpMethods.Post || httpMethod == HttpMethods.Put || httpMethod == HttpMethods.Patch)
        {
            parameters = await ReadRequestBodyAsync(context);
        }

        string userId = context.User?.Identity?.IsAuthenticated == true
            ? context.User.Identity?.Name ?? "Unknown"
            : "Anonymous";

        string userIp = context.Connection.RemoteIpAddress?.ToString() ?? "N/A";


        using (LogContext.PushProperty("RequestId", requestId))
        using (LogContext.PushProperty("HttpMethod", httpMethod))
        using (LogContext.PushProperty("Controller", controller))
        using (LogContext.PushProperty("Action", action))
        using (LogContext.PushProperty("Parameters", parameters))
        using (LogContext.PushProperty("UserId", userId))
        using (LogContext.PushProperty("UserIp", userIp))
        {
            await _next(context);

            sw.Stop();


        }
        // Format duration as HH:mm:ss.fff
        string formattedDuration = TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds)
                                        .ToString(@"hh\:mm\:ss\.fff");

        using (LogContext.PushProperty("Duration", formattedDuration))
        using (LogContext.PushProperty("StatusCode", context.Response?.StatusCode))
        {

            // Serilog picks up properties automatically
        }

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
        context.Request.Body.Position = 0; // rewind so controller can still read

        return body;
    }
}

