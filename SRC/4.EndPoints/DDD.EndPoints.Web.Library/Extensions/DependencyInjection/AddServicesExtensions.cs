using DDD.Utilities.Library;

namespace DDD.EndPoints.Web.Library.Extensions.DependencyInjection;
public static class AddServicesExtensions
{
    /// <summary>
    /// 
    /// ======================================
    /// بعد از مرحله چهارم اجرا میشود
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddUtilityServices(
        this IServiceCollection services)
    {
        services.AddTransient<UtilitiesServices>();
        return services;
    }
}