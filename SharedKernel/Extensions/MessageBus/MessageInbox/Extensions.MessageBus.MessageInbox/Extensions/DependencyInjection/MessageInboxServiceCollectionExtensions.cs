using Extensions.MessageBus.MessageInbox.Extensions.DependencyInjection;
using Extensions.MessageBus.MessageInbox.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Extensions.MessageBus.Abstractions;

namespace Extensions.MessageBus.MessageInbox.Extensions.DependencyInjection;

public static class MessageInboxServiceCollectionExtensions
{
    public static IServiceCollection AddMessageInbox(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MessageInboxOptions>(configuration);
        AddServices(services);
        return services;
    }

    public static IServiceCollection AddMessageInbox(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddMessageInbox(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddMessageInbox(this IServiceCollection services, Action<MessageInboxOptions> setupAction)
    {
        services.Configure(setupAction);
        AddServices(services);
        return services;
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddScoped<IMessageConsumer, InboxMessageConsumer>();
    }
}