﻿using Extensions.ChangeDataLog.Sql.Extensions.DependencyInjection;
using Extensions.ChangeDataLog.Sql.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Extensions.ChangeDataLog.Abstractions;

namespace Extensions.ChangeDataLog.Sql.Extensions.DependencyInjection;

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