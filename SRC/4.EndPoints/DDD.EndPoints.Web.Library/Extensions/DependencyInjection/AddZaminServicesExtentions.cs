using DDD.Utilities.Library;

namespace DDD.EndPoints.Web.Library.Extensions.DependencyInjection;
public static class AddZaminServicesExtensions
{
    public static IServiceCollection AddZaminUntilityServices(
        this IServiceCollection services)
    {
        services.AddTransient<UtilitiesServices>();
        return services;
    }
}