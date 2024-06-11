using Microsoft.Extensions.DependencyInjection;
using Extensions.Serializers.Abstractions;
using Extensions.Serializers.EPPlus.Services;

namespace Extensions.Serializers.EPPlus.Extensions.DependencyInjection;

public static class EPPlusExcelSerializerServiceCollectionExtensions
{
    public static IServiceCollection AddEPPlusExcelSerializer(this IServiceCollection services)
        => services.AddSingleton<IExcelSerializer, EPPlusExcelSerializer>();
}