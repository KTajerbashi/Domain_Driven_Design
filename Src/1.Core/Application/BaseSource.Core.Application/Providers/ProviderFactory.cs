using BaseSource.Core.Application.Common.Handlers.Event;
using BaseSource.Core.Application.Interfaces;
using BaseSource.Core.Application.Providers.CacheSystem;
using BaseSource.Core.Application.Providers.DapperQuery;
using BaseSource.Core.Application.Providers.MapperObjects;

namespace BaseSource.Core.Application.Providers;

public class ProviderFactory(
    IJsonSerializer jsonSerializer,
    ICacheAdapter cacheAdapter,
    IQueryExecute query,
    IMapperAdapter mapper,
    IUserSystem user,
    ICurrentUserServiceFactory userFactory,
    IDomainEventDispatcher eventDispatcher,
    IMediator mediator
    //ILoggerFactory loggerFactory,
    //IIdentity identity,
    //IIdentityLogger identityLogger,
    //IManagedIdentityApplication managedIdentityApplication
    )
{
    public IJsonSerializer Serializer = jsonSerializer;
    public ICacheAdapter Cache = cacheAdapter;
    public IQueryExecute Query = query;
    public IMapperAdapter Mapper = mapper;
    public IUserSystem User = user;
    public ICurrentUserServiceFactory UserFactory = userFactory;
    public IDomainEventDispatcher EventDispatcher = eventDispatcher;
    public IMediator Mediator = mediator;
    //public ILoggerFactory LoggerFactory = loggerFactory;
    //public IIdentity Identity = identity;
    //public IIdentityLogger IdentityLogger = identityLogger;
    //public IManagedIdentityApplication ManagedIdentityApplication = managedIdentityApplication;

}
