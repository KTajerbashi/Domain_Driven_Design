using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Extensions.UsersManagement.Abstractions;
using Extensions.UsersManagement.Options;
using Extensions.UsersManagement.Extensions.DependencyInjection;
using Extensions.UsersManagement.Services;

namespace Extensions.UsersManagement.Extensions.DependencyInjection;

public static class UserInfoServiceCollectionExtensions
{
    public static IServiceCollection AddWebUserInfoService(this IServiceCollection services, IConfiguration configuration, bool useFake = false)
    {
        if (useFake)
        {
            services.AddSingleton<IUserInfoService, FakeUserInfoService>();

        }
        else
        {
            services.Configure<UserManagementOptions>(configuration);
            services.AddSingleton<IUserInfoService, WebUserInfoService>();

        }
        return services;
    }


    public static IServiceCollection AddWebUserInfoService(this IServiceCollection services, IConfiguration configuration, string sectionName, bool useFake = false)
    {
        services.AddWebUserInfoService(configuration.GetSection(sectionName), useFake);
        return services;
    }

    public static IServiceCollection AddWebUserInfoService(this IServiceCollection services, Action<UserManagementOptions> setupAction, bool useFake = false)
    {
        if (useFake)
        {
            services.AddSingleton<IUserInfoService, FakeUserInfoService>();

        }
        else
        {
            services.Configure(setupAction);
            services.AddSingleton<IUserInfoService, WebUserInfoService>();

        }
        return services;
    }
}

