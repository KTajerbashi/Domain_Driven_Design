using BaseSource.Core.Application;
using BaseSource.EndPoint.WebApi.Extensions;
using BaseSource.EndPoint.WebApi.Providers.Identity;
using BaseSource.Infrastructure.SQL.Command;
using BaseSource.Infrastructure.SQL.Query;

namespace BaseSource.EndPoint.WebApi;

public static class DependencyInjections
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        //  Get Assemblies Of By Namespaces
        var assemblies = ("BaseSource").GetAssemblies().ToArray();

        //  Add Application Libraries
        services.AddApplicationServices(configuration, assemblies);

        //  Add Command Infrastructure
        services.AddInfrastructureCommandsServices(configuration, "CommandConnection");
        services.AddInfrastructureQueriesServices(configuration, "QueryConnection");

        services.AddIdentityServices(configuration, "Identity");

        return services;
    }

}