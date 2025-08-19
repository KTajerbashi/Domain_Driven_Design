using BaseSource.EndPoint.WebApi;

var builder = WebApplication.CreateBuilder(args);
var app = builder
    .AddSerilog()
    .AddWebAppServices()
    .AddWebApiServices()
    .Build();

await app
    .UseWebAppServices()
    .InitialiseDatabaseAsync();
app.Run();
