using Extensions.ChangeDataLog.Sql.Extensions.DependencyInjection;
using Extensions.ChangeDataLog.Sql.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Extensions.ChangeDataLog.Abstractions;

namespace Extensions.ChangeDataLog.Sql.Extensions.DependencyInjection;

public static class ChageDatalogServiceCollectionExtensions
{
    public static IServiceCollection AddZaminChageDatalogDalSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEntityChageInterceptorItemRepository, DapperEntityChageInterceptorItemRepository>();
        services.Configure<ChangeDataLogSqlOptions>(configuration);
        return services;
    }

    public static IServiceCollection AddZaminChageDatalogDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddZaminChageDatalogDalSql(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddZaminChageDatalogDalSql(this IServiceCollection services, Action<ChangeDataLogSqlOptions> setupAction)
    {
        services.AddScoped<IEntityChageInterceptorItemRepository, DapperEntityChageInterceptorItemRepository>();
        services.Configure(setupAction);
        return services;
    }
}