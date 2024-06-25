
using MiniBlog.EndPoints.API.Extensions;
using SerilogRegistration.Extensions;
using SerilogRegistration.Extensions.DependencyInjection;

///این 
SerilogExtensions.RunWithSerilogExceptionHandling(() =>
{
    var builder = WebApplication.CreateBuilder(args);
    var app = builder.AddTKSerilog(o =>
    {
        o.ApplicationName = builder.Configuration.GetValue<string>("ApplicationName");
        o.ServiceId = builder.Configuration.GetValue<string>("ServiceId");
        o.ServiceName = builder.Configuration.GetValue<string>("ServiceName");
        o.ServiceVersion = builder.Configuration.GetValue<string>("ServiceVersion");
    }).ConfigureServices().ConfigurePipeline();
    app.Run();
});