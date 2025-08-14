using BaseSource.Infrastructure.SQL.Command.Persistence;
using BaseSource.Infrastructure.SQL.Common.Persistence.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseSource.Infrastructure.SQL.Command;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureCommandsServices(this IServiceCollection services, IConfiguration configuration,string connectionString)
    {
        services.AddDbContext<CommandDatabaseContext>(options =>
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
                options.MigrationsAssembly(typeof(CommandDatabaseContext).Assembly.FullName);
                options.EnableRetryOnFailure();
                options.CommandTimeout(60);
            })
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging();

            options.AddInterceptors(new AddAuditDataInterceptor());
        });

        services.AddScoped<InitialDatabaseContext>();

        return services;
    }
}
