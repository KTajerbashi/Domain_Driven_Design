using BaseSource.Core.Application.Interfaces;
using BaseSource.Core.Application.Providers.CacheSystem;
using BaseSource.Core.Application.Providers.MapperObjects;

namespace BaseSource.Core.Application.Providers;

public class ProviderFactory(
    IJsonSerializer jsonSerializer,
    ICacheAdapter cacheAdapter,
    IMapperAdapter mapper,
    IUserSystem user,
    ICurrentUserServiceFactory userFactory,
    IMediator mediator
    //ILoggerFactory loggerFactory,
    //IIdentity identity,
    //IIdentityLogger identityLogger,
    //IManagedIdentityApplication managedIdentityApplication
    )
{
    public IJsonSerializer Serializer = jsonSerializer;
    public ICacheAdapter Cache = cacheAdapter;
    public IMapperAdapter Mapper = mapper;
    public IUserSystem User = user;
    public ICurrentUserServiceFactory UserFactory = userFactory;
    public IMediator Mediator = mediator;
    //public ILoggerFactory LoggerFactory = loggerFactory;
    //public IIdentity Identity = identity;
    //public IIdentityLogger IdentityLogger = identityLogger;
    //public IManagedIdentityApplication ManagedIdentityApplication = managedIdentityApplication;

}
