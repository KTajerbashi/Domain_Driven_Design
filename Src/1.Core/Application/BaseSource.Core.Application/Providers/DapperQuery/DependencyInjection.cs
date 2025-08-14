using Microsoft.Extensions.Configuration;

namespace BaseSource.Core.Application.Providers.DapperQuery;

public static class DependencyInjection
{
    public static IServiceCollection AddQueryExecute(
        this IServiceCollection services, 
        IConfiguration configuration,
        string connectionName)
    {
        services.AddScoped<IDbConnection>(provider =>
        {
            var connection = new SqlConnection(); // Use your specific DbConnection here
            connection.ConnectionString = configuration.GetConnectionString(connectionName);
            return connection;
        });

        //services.AddScoped<IQueryExecute, QueryExecute>();

        return services;
    }
}
