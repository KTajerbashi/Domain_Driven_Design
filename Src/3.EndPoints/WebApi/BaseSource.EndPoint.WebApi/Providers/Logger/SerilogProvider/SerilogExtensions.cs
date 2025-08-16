using BaseSource.Kernel.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Serilog;
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
        return builder;
    }

    public static WebApplication UseSerilog(this WebApplication app)
    {
        return app;
    }
}
