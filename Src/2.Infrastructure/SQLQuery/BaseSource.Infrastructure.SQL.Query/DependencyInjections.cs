using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseSource.Infrastructure.SQL.Query;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureQueriesServices(this IServiceCollection service, IConfiguration configuration)
    {

        return service;
    }
}
