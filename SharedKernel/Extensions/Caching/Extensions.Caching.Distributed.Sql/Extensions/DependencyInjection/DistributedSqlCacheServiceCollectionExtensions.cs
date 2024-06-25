using Dapper;
using Extensions.Caching.Abstractions;
using Extensions.Caching.Distributed.Sql.Options;
using Extensions.Caching.Distributed.Sql.Services;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Extensions.Caching.Distributed.Sql.Extensions.DependencyInjection;

public static class DistributedSqlCacheServiceCollectionExtensions
{
    /// <summary>
    ///  برای تزریق کردن در سرویس های دیگر
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="sectionName"></param>
    /// <returns></returns>
    public static IServiceCollection AddKernelSqlDistributedCache(this IServiceCollection services,
                                                                  IConfiguration configuration,
                                                                  string sectionName)
        => services.AddKernelSqlDistributedCache(configuration.GetSection(sectionName));
    /// <summary>
    /// برای تزریق داخلی
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddKernelSqlDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICacheAdapter, DistributedSqlCacheAdapter>();
        services.Configure<DistributedSqlCacheOptions>(configuration);

        ///خواندن مدل مورد نظر از appsettings.json
        var option = configuration.Get<DistributedSqlCacheOptions>();

        if (option.AutoCreateTable)
            CreateTable(option);

        services.AddDistributedSqlServerCache(options =>
        {
            options.ConnectionString = option.ConnectionString;
            options.SchemaName = option.SchemaName;
            options.TableName = option.TableName;
        });

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddKernelSqlDistributedCache(this IServiceCollection services,
                                                            Action<DistributedSqlCacheOptions> setupAction)
    {
        services.AddTransient<ICacheAdapter, DistributedSqlCacheAdapter>();
        services.Configure(setupAction);

        var option = new DistributedSqlCacheOptions();
        setupAction.Invoke(option);

        if (option.AutoCreateTable)
            CreateTable(option);

        services.AddDistributedSqlServerCache(options =>
        {
            options.ConnectionString = option.ConnectionString;
            options.SchemaName = option.SchemaName;
            options.TableName = option.TableName;
        });

        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    private static void CreateTable(DistributedSqlCacheOptions options)
    {
        string table = options.TableName;
        string schema = options.SchemaName;

        string createTable =
            $@"
IF (NOT EXISTS (SELECT *  FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = '{schema}' AND  TABLE_NAME = '{table}' ))
Begin
CREATE TABLE [{schema}].[{table}](
[Id][nvarchar](449) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
[Value] [varbinary](max)NOT NULL,
[ExpiresAtTime] [datetimeoffset](7) NOT NULL,
[SlidingExpirationInSeconds] [bigint] NULL,
[AbsoluteExpiration] [datetimeoffset](7) NULL,
PRIMARY KEY(Id),
INDEX Index_ExpiresAtTime NONCLUSTERED (ExpiresAtTime))
End;
";

        var dbConnection = new SqlConnection(options.ConnectionString);
        dbConnection.Execute(createTable);
    }
}