using DDD.Utilities.Library;

namespace DDD.EndPoints.Web.Library.Extensions.DependencyInjection;
public static class AddServicesExtensions
{
    public static IServiceCollection AddUntilityServices(
        this IServiceCollection services)
    {
        services.AddTransient<UtilitiesServices>();
        return services;
    }
}