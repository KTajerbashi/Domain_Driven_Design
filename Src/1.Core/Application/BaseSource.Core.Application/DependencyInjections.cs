using BaseSource.Core.Application.Common.Handlers.Behaviors;
using BaseSource.Core.Application.Common.Repositories;
using BaseSource.Core.Application.Providers;
using BaseSource.Core.Application.Providers.Autofac;
using BaseSource.Core.Application.Providers.CacheSystem;
using BaseSource.Core.Application.Providers.DapperQuery;
using BaseSource.Core.Application.Providers.MapperObjects;

namespace BaseSource.Core.Application;

public static class DependencyInjections
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration,
        Assembly[] assemblies)
    {
        //  Cache
        services.AddSqlDistributedCache(configuration, "SqlCache");

        //  Dapper
        services.AddQueryExecute(configuration, "Query");

        //  Mapper
        services.AddAutoMapperProfiles(configuration, assemblies);

        //  scrutor
        services.AddScrutorProvider(assemblies);

        //  Serializer
        services.AddMicrosoftSerializer();

        //  Factory
        services.AddScoped<ProviderFactory>();

        //  Repositories
        services.AddRepositories(assemblies);

        //  MediateR
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(assemblies);

            // Register behaviors in specific order
            cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
            cfg.AddOpenBehavior(typeof(PerformanceBehavior<,>));
            cfg.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            //cfg.AddOpenBehavior(typeof(TransactionBehavior<,>));
            //cfg.AddOpenBehavior(typeof(DomainEventDispatchBehavior<,>));
        });

        //  Fluent Validation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


        return services;
    }

}
