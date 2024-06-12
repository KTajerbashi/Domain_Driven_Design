using Extensions.Caching.InMemory.Services;
using Microsoft.Extensions.DependencyInjection;
using Extensions.Caching.Abstractions;

namespace Extensions.Caching.InMemory.Extensions.DependencyInjection;

public static class InMemoryCachingServiceCollectionExtensions
{
    public static IServiceCollection AddInMemoryCaching(this IServiceCollection services)
        => services.AddMemoryCache().AddTransient<ICacheAdapter, InMemoryCacheAdapter>();
}