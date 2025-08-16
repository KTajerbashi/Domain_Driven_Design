using BaseSource.EndPoint.WebApi.Providers.Logger.SystemEventProvider;

namespace BaseSource.EndPoint.WebApi.Providers.Logger;

public static class DependencyInjections
{
    private static readonly Action<ILogger, int, Exception?> _userLoggedIn = 
        LoggerMessage.Define<int>(LogLevel.Information,new EventId(1, "UserLoggedIn"),"User {UserId} logged in");


    public static IServiceCollection AddLoggerServices(this IServiceCollection services, IConfiguration configuration)
    {
        //_userLoggedIn(logger, userId, null);

        services.AddSingleton<ISystemEventLogger, SystemEventLogger>();
        return services;
    }
}
