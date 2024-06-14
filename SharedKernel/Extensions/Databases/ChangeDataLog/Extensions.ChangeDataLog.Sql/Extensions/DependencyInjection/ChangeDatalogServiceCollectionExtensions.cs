﻿using Extensions.ChangeDataLog.Abstractions;
using Extensions.ChangeDataLog.Sql.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Extensions.ChangeDataLog.Sql.Extensions.DependencyInjection;

/// <summary>
/// تزریق کننده سرویس تغییرات
/// </summary>
public static class ChangeDatalogServiceCollectionExtensions
{
    public static IServiceCollection AddChangeDatalogDalSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IEntityChangeInterceptorItemRepository, DapperEntityChageInterceptorItemRepository>();
        services.Configure<ChangeDataLogSqlOptions>(configuration);
        return services;
    }

    public static IServiceCollection AddChangeDatalogDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddChangeDatalogDalSql(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddChangeDatalogDalSql(this IServiceCollection services, Action<ChangeDataLogSqlOptions> setupAction)
    {
        services.AddScoped<IEntityChangeInterceptorItemRepository, DapperEntityChageInterceptorItemRepository>();
        services.Configure(setupAction);
        return services;
    }
}