using BaseSource.Core.Application.Interfaces;
using BaseSource.Core.Application.Providers.CacheSystem;
using BaseSource.Core.Application.Providers.MapperObjects;

namespace BaseSource.Core.Application.Providers;

public class ProviderFactory(
    IJsonSerializer jsonSerializer, 
    ICacheAdapter cacheAdapter, 
    IMapperAdapter mapper,
    IUser user
    )
{
    public IJsonSerializer Serializer = jsonSerializer;
    public ICacheAdapter Cache = cacheAdapter;
    public IMapperAdapter Mapper = mapper;
    public IUser User = user;
}
