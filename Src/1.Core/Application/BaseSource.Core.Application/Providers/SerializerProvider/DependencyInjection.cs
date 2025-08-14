using Microsoft.Extensions.DependencyInjection;

namespace BaseSource.Core.Application.Providers.SerializerProvider;

public static class DependencyInjection
{
    public static IServiceCollection AddMicrosoftSerializer(this IServiceCollection services)
    => services.AddSingleton<IJsonSerializer, MicrosoftSerializer>();
}