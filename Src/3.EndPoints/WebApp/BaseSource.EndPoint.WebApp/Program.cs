using Autofac;
using Autofac.Extensions.DependencyInjection;
using BaseSource.Core.Application.Providers.Autofac;
using BaseSource.EndPoint.WebApi.Extensions;
using BaseSource.EndPoint.WebApi.Providers.Swagger;

var builder = WebApplication.CreateBuilder(args);

var assemblies = ("BaseSource").GetAssemblies().ToArray();

var configuration = builder.Configuration;
builder.Services.AddSwaggerProvider(configuration);
builder.Services.AddWebAppServices(configuration, assemblies);
// Configure Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.AddAutofacLifetimeServices(assemblies);
    });
var app = builder.Build().UseWebAppServices();
await app.InitialiseDatabaseAsync();
app.Run();
