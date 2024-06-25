using Extensions.Caching.InMemory.Services;
using Microsoft.Extensions.DependencyInjection;
using Extensions.Caching.Abstractions;

namespace Extensions.Caching.InMemory.Extensions.DependencyInjection;

/// <summary>
/// در ادامه مرحله چهارم
/// </summary>
public static class InMemoryCachingServiceCollectionExtensions
{
    public static IServiceCollection AddKernelInMemoryCaching(this IServiceCollection services)
        => services.AddMemoryCache().AddTransient<ICacheAdapter, InMemoryCacheAdapter>();
}