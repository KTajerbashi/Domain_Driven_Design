using Microsoft.Extensions.DependencyInjection;
using Extensions.Caching.InMemory.Services;
using Extensions.Caching.Abstractions;

namespace Extensions.DependencyInjection;

public static class InMemoryCachingServiceCollectionExtensions
{
    public static IServiceCollection AddZaminInMemoryCaching(this IServiceCollection services)
        => services.AddMemoryCache().AddTransient<ICacheAdapter, InMemoryCacheAdapter>();
}