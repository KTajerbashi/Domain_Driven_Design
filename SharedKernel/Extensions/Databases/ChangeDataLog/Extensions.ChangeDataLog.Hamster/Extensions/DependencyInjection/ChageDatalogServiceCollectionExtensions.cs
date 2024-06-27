using Extensions.ChangeDataLog.Hamster.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Extensions.ChangeDataLog.Hamster.Extensions.DependencyInjection;

/// <summary>
/// سرویس تزریق کننده 
/// Change Interceptor
/// </summary>
public static class ChangeDataLogServiceCollectionExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection AddHamsterChangeDataLog(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.Configure<ChangeDataLogHamsterOptions>(configuration);
        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <param name="sectionName"></param>
    /// <returns></returns>
    public static IServiceCollection AddHamsterChangeDataLog(this IServiceCollection services,
        IConfiguration configuration,
        string sectionName)
    {
        services.AddHamsterChangeDataLog(configuration.GetSection(sectionName));
        return services;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="services"></param>
    /// <param name="setupAction"></param>
    /// <returns></returns>
    public static IServiceCollection AddHamsterChangeDataLog(this IServiceCollection services, 
        Action<ChangeDataLogHamsterOptions> setupAction)
    {
        services.Configure(setupAction);
        return services;
    }
}