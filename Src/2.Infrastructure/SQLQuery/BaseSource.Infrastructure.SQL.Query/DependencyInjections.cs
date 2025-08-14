using BaseSource.Infrastructure.SQL.Common.Persistence.Interceptors;
using BaseSource.Infrastructure.SQL.Query.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BaseSource.Infrastructure.SQL.Query;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureQueriesServices(this IServiceCollection services, IConfiguration configuration, string connectionString)
    {
        services.AddDbContext<QueryDatabaseContext>(options =>
        {
            //options.UseSqlServer(configuration.GetConnectionString(connectionString),
            //    sqlOptions =>
            //    {
            //        sqlOptions.MigrationsAssembly(typeof(CommandDatabaseContext).Assembly.FullName);
            //        sqlOptions.EnableRetryOnFailure(
            //            maxRetryCount: 5,
            //            maxRetryDelay: TimeSpan.FromSeconds(30),
            //            errorNumbersToAdd: null);
            //    });

            options.UseSqlServer(configuration.GetConnectionString(connectionString), options =>
            {
                options.MigrationsAssembly(typeof(QueryDatabaseContext).Assembly.FullName);
                options.EnableRetryOnFailure();
                options.CommandTimeout(60);
            })
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();

            options.AddInterceptors(new AddAuditDataInterceptor());
        });

        return services;
    }
}
