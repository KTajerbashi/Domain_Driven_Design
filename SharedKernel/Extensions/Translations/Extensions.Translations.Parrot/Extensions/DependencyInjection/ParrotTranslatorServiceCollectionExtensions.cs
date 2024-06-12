using Extensions.Translations.Abstractions;
using Extensions.Translations.Parrot.Options;
using Extensions.Translations.Parrot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Extensions.Translations.Parrot.Extensions.DependencyInjection;

public static class ParrotTranslatorServiceCollectionExtensions
{
    public static IServiceCollection AddParrotTranslator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ITranslator, ParrotTranslator>();
        services.Configure<ParrotTranslatorOptions>(configuration);
        return services;
    }

    public static IServiceCollection AddParrotTranslator(this IServiceCollection services, IConfiguration configuration, string sectionName)
    {
        services.AddParrotTranslator(configuration.GetSection(sectionName));
        return services;
    }

    public static IServiceCollection AddParrotTranslator(this IServiceCollection services, Action<ParrotTranslatorOptions> setupAction)
    {
        services.AddSingleton<ITranslator, ParrotTranslator>();
        services.Configure(setupAction);
        return services;
    }
}