
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddWebAppServices(builder.Configuration);

var app = builder.Build();
app.UseWebAppServices();
app.Run();
