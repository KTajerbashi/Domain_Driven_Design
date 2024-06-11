using Microsoft.Extensions.Logging;
using Extensions.ObjectMappers.Abstractions;
using Extensions.Serializers.Abstractions;
using Extensions.Translations.Abstractions;
using Extensions.UsersManagement.Abstractions;
using Extensions.Caching.Abstractions;
namespace DDD.Utilities.Library;

public class UtilitiesServices
{
    public readonly ITranslator Translator;
    public readonly ICacheAdapter CacheAdapter;
    public readonly IMapperAdapter MapperFacade;
    public readonly ILoggerFactory LoggerFactory;
    public readonly IJsonSerializer Serializer;
    public readonly IUserInfoService UserInfoService;

    public UtilitiesServices(ITranslator translator,
            ILoggerFactory loggerFactory,
            IJsonSerializer serializer,
            IUserInfoService userInfoService,
            ICacheAdapter cacheAdapter,
            IMapperAdapter mapperFacade)
    {
        Translator = translator;
        LoggerFactory = loggerFactory;
        Serializer = serializer;
        UserInfoService = userInfoService;
        CacheAdapter = cacheAdapter;
        MapperFacade = mapperFacade;
    }
}
