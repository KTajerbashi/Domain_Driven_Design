using Microsoft.Extensions.DependencyInjection;
using Extensions.Serializers.Abstractions;
using Extensions.Serializers.NewtonSoft.Services;

namespace Extensions.Serializers.NewtonSoft.Extensions.DependencyInjection;

public static class NewtonSoftSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddSNewtonSoftSerializer(this IServiceCollection services)
        => services.AddSingleton<IJsonSerializer, NewtonSoftSerializer>();
}