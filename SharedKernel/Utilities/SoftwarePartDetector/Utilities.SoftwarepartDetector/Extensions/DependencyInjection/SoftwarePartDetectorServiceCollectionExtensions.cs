using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Utilities.SoftwarepartDetector.Authentications;
using Utilities.SoftwarepartDetector.Detectors;
using Utilities.SoftwarepartDetector.Extensions.DependencyInjection;
using Utilities.SoftwarepartDetector.Options;
using Utilities.SoftwarepartDetector.Publishers;
using Utilities.SoftwarepartDetector.Services;

namespace Utilities.SoftwarepartDetector.Extensions.DependencyInjection;
public static class SoftwarePartDetectorServiceCollectionExtensions
{
    public static IServiceCollection AddSoftwarePartDetector(this IServiceCollection services, IConfiguration configuration, string sectionName)
        => services.AddSoftwarePartDetector(configuration.GetSection(sectionName));

    public static IServiceCollection AddSoftwarePartDetector(this IServiceCollection services, IConfiguration configuration)
        => services.AddServices(configuration.Get<SoftwarePartDetectorOptions>()).Configure<SoftwarePartDetectorOptions>(configuration);

    public static IServiceCollection AddSoftwarePartDetector(this IServiceCollection services, Action<SoftwarePartDetectorOptions> setupAction)
    {
        var option = new SoftwarePartDetectorOptions();

        setupAction.Invoke(option);

        return services.AddServices(option).Configure(setupAction);
    }

    private static IServiceCollection AddServices(this IServiceCollection services, SoftwarePartDetectorOptions option)
    {
        services.AddTransient<ControllersAndActionDetector>();
        services.AddTransient<SoftwarePartDetector>();
        services.AddTransient<ISoftwarePartPublisher, SoftwarePartWebPublisher>();
        services.AddTransient<ISoftwarePartAuthentication, SoftwarePartAuthentication>();
        services.AddTransient<SoftwarePartDetectorService>();

        var publisherHttpClient = services.AddHttpClient<ISoftwarePartPublisher, SoftwarePartWebPublisher>();
        var oidcHttpClient = services.AddHttpClient<ISoftwarePartAuthentication, SoftwarePartAuthentication>();

        if (option.FakeSSL)
        {
            publisherHttpClient.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true });
            oidcHttpClient.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true });
        }

        return services;
    }
}