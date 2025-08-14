using Autofac.Core;
using BaseSource.Core.Application.Providers;
using BaseSource.Core.Application.Providers.Autofac;
using BaseSource.Core.Application.Providers.CacheSystem;
using BaseSource.Core.Application.Providers.DapperQuery;
using BaseSource.Core.Application.Providers.MapperObjects;

namespace BaseSource.Core.Application;

public class ApplicationOption
{
    public string CacheSection { get; set; }
    public string QueryConnection { get; set; }
}


public static class DependencyInjections
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services, 
        IConfiguration configuration,
        Assembly[] assemblies)
    {
        //  Autofac
        services.BuildAutofacServiceProvider(assemblies);

        //  Cache
        services.AddSqlDistributedCache(configuration, "SqlCache");

        //  Dapper
        services.AddQueryExecute(configuration,"Query");

        //  Mapper
        services.AddAutoMapperProfiles(configuration,assemblies);

        //  scrutor
        services.AddScrutorProvider(assemblies);

        //  Serializer
        services.AddMicrosoftSerializer();
        
        //  Factory
        services.AddScoped<ProviderFactory>();

        return services;
    }

}
