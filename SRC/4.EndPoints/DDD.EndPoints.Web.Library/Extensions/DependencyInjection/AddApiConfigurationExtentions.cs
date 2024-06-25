using DDD.EndPoints.Web.Library.Middlewares.ApiExceptionHandler;
using FluentValidation.AspNetCore;

namespace DDD.EndPoints.Web.Library.Extensions.DependencyInjection;

/// <summary>
/// 
/// </summary>
public static class AddApiConfigurationExtensions
{
    /// <summary>
    /// درین ساختار وابستگی های زیر ساخت تزریق میشود
    /// =========================================
    /// بعد از مرحله چهارم اجرا میشود
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblyNamesForLoad"></param>
    /// <returns></returns>
    public static IServiceCollection AddWebApiCore(this IServiceCollection services, params string[] assemblyNamesForLoad)
    {
        services
            .AddControllers()
            .AddFluentValidation();
        services.AddDependencies(assemblyNamesForLoad);

        return services;
    }

    /// <summary>
    /// میدلور های مدیریت خطا درین اکستنشن تزریق میشود
    /// ==============
    /// در ادامه مرحله پنجم گام اول
    /// </summary>
    /// <param name="app"></param>

    public static void UseApiExceptionHandler(this IApplicationBuilder app)
    {
        app.UseApiExceptionHandler(options =>
        {
            options.AddResponseDetails = (context, ex, error) =>
            {
                if (ex.GetType().Name == typeof(Microsoft.Data.SqlClient.SqlException).Name)
                {
                    error.Detail = "Exception was a database exception!";
                }
            };
            options.DetermineLogLevel = ex =>
            {
                if (ex.Message.StartsWith("cannot open database", StringComparison.InvariantCultureIgnoreCase) ||
                    ex.Message.StartsWith("a network-related", StringComparison.InvariantCultureIgnoreCase))
                {
                    return LogLevel.Critical;
                }
                return LogLevel.Error;
            };
        });
    }



}