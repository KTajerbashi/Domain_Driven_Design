using BaseSource.EndPoint.WebApi.Providers.Swagger;

namespace BaseSource.EndPoint.WebApi;

public static class DependencyInjections
{
    public static WebApplicationBuilder AddWebApiServices(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;

        //  Get Assemblies Of By Namespaces
        var assemblies = ("BaseSource").GetAssemblies().ToArray();

        builder.Services.AddSwaggerProvider(configuration);

        // Configure Autofac
        builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(containerBuilder =>
            {
                containerBuilder.AddAutofacLifetimeServices(assemblies);
            });

        //  Add Application Libraries
        builder.Services.AddApplicationServices(configuration, assemblies);

        //  Add Command Infrastructure
        builder.Services.AddInfrastructureCommandsServices(configuration, "CommandConnection");
        builder.Services.AddInfrastructureQueriesServices(configuration, "QueryConnection");

        //  Add Identity
        builder.Services.AddIdentityServices(configuration, "Identity");

        //  Add System Logger
        builder.Services.AddLoggerServices(configuration);

        return builder;
    }

}