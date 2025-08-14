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
        this IServiceCollection service, 
        IConfiguration configuration,
        Assembly[] assemblies)
    {
        //  Autofac
        service.BuildAutofacServiceProvider(assemblies);

        //  Cache
        service.AddSqlDistributedCache(configuration, "SqlCache");

        //  Dapper
        service.AddQueryExecute(configuration,"Query");

        //  Mapper
        service.AddAutoMapperProfiles(configuration,assemblies);

        //  scrutor
        service.AddScrutorProvider(assemblies);

        //  Serializer
        service.AddMicrosoftSerializer();

        return service;
    }

}
