using DDD.EndPoints.Web.Library.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace DDD.EndPoints.Web.Library.Extensions.ModelBinding;


/// <summary>
/// 
/// </summary>
public static class NonValidatingValidatorExtensions
{
    /// <summary>
    /// در ادامه مرحله چهارم
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddNonValidatingValidator(this IServiceCollection services)
    {
        var validator = services.FirstOrDefault(s => s.ServiceType == typeof(IObjectModelValidator));
        if (validator != null)
        {
            services.Remove(validator);
            services.Add(new ServiceDescriptor(typeof(IObjectModelValidator), _ => new NonValidatingValidator(), ServiceLifetime.Singleton));
        }
        return services;
    }
}
