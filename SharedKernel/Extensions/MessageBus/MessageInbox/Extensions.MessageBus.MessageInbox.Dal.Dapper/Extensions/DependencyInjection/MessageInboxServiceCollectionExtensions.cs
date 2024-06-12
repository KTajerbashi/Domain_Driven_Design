using Extensions.MessageBus.MessageInbox.Dal.Dapper.Extensions.DependencyInjection;
using Extensions.MessageBus.MessageInbox.Dal.Dapper.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Extensions.MessageBus.Abstractions;

namespace Extensions.MessageBus.MessageInbox.Dal.Dapper.Extensions.DependencyInjection;

public static class MessageInboxServiceCollectionExtensions
{
    public static IServiceCollection AddMessageInboxDalSql(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageInboxDalDapperOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddMessageInboxDalSql(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddMessageInboxDalSql(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddMessageInboxDalSql(this IServiceCollection services, Action<MessageInboxDalDapperOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddSingleton<IMessageInboxItemRepository, SqlMessageInboxItemRepository>();
    }
}