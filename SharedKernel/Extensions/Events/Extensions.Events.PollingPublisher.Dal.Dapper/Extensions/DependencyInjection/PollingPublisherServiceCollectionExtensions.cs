using Extensions.Events.Abstractions;
using Extensions.Events.PollingPublisher.Dal.Dapper.DataAccess;
using Extensions.Events.PollingPublisher.Dal.Dapper.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Extensions.Events.PollingPublisher.Dal.Dapper.Extensions.DependencyInjection;
public static class PollingPublisherServiceCollectionExtensions
{
    public static IServiceCollection AddPollingPublisherDalSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PollingPublisherDalRedisOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddPollingPublisherDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddPollingPublisherDalSql(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddPollingPublisherDalSql(this IServiceCollection services, Action<PollingPublisherDalRedisOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IOutBoxEventItemRepository, SqlOutBoxEventItemRepository>();
    }
}