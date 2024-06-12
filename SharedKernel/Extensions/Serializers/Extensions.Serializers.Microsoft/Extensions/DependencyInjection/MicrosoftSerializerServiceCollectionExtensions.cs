using Extensions.Serializers.Microsoft.Services;
using Microsoft.Extensions.DependencyInjection;
using Extensions.Serializers.Abstractions;

namespace Extensions.Serializers.Microsoft.Extensions.DependencyInjection;

public static class MicrosoftSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddMicrosoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IJsonSerializer, MicrosoftSerializer>();
}
