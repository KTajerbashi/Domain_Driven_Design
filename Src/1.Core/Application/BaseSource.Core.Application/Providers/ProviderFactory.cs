using BaseSource.Core.Application.Interfaces;
using BaseSource.Core.Application.Providers.CacheSystem;
using BaseSource.Core.Application.Providers.MapperObjects;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Abstractions;
using System.Security.Principal;

namespace BaseSource.Core.Application.Providers;

public class ProviderFactory(
    IJsonSerializer jsonSerializer,
    ICacheAdapter cacheAdapter,
    IMapperAdapter mapper,
    IUserSystem user,
    ICurrentUserServiceFactory userFactory,
    ILoggerFactory loggerFactory,
    IIdentity identity,
    IIdentityLogger identityLogger,
    IManagedIdentityApplication managedIdentityApplication
    )
{
    public IJsonSerializer Serializer = jsonSerializer;
    public ICacheAdapter Cache = cacheAdapter;
    public IMapperAdapter Mapper = mapper;
    public IUserSystem User = user;
    public ICurrentUserServiceFactory UserFactory = userFactory;
    public ILoggerFactory LoggerFactory = loggerFactory;
    public IIdentity Identity = identity;
    public IIdentityLogger IdentityLogger = identityLogger;
    public IManagedIdentityApplication ManagedIdentityApplication = managedIdentityApplication;

}
