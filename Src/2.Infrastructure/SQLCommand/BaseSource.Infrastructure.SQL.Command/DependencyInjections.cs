using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BaseSource.Infrastructure.SQL.Command;

public static class DependencyInjections
{
    public static IServiceCollection AddInfrastructureCommandsServices(this IServiceCollection service, IConfiguration configuration)
    {

        return service;
    }
}
