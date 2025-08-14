using BaseSource.Core.Application.Providers.CacheSystem.SQL;

namespace BaseSource.Core.Application.Providers.CacheSystem;

public static class DependencyInjections
{
    public static IServiceCollection AddSqlDistributedCache(this IServiceCollection services,
                                                                      IConfiguration configuration,
                                                                      string sectionName)
            => services.AddSqlDistributedCache(configuration.GetSection(sectionName));

    public static IServiceCollection AddSqlDistributedCache(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ICacheAdapter, SqlCacheAdapter>();
        services.Configure<SqlCacheOptions>(configuration);

        var option = configuration.Get<SqlCacheOptions>();

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

    public static IServiceCollection AddSqlDistributedCache(this IServiceCollection services,
                                                            Action<SqlCacheOptions> setupAction)
    {
        services.AddTransient<ICacheAdapter, SqlCacheAdapter>();
        services.Configure(setupAction);

        var option = new SqlCacheOptions();
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

    private static void CreateTable(SqlCacheOptions options)
    {
        string table = options.TableName;
        string schema = options.SchemaName;

        string createTable = $@"
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = '{schema}')
BEGIN
    EXEC('CREATE SCHEMA {schema}');
END

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
	End
";
        var dbConnection = new SqlConnection(options.ConnectionString);
        dbConnection.Execute(createTable);
    }
}