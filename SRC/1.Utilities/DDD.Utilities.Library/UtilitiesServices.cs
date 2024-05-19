using Microsoft.Extensions.Logging;
using Zamin.Extensions.Caching.Abstractions;
using Zamin.Extensions.ObjectMappers.Abstractions;
using Zamin.Extensions.Serializers.Abstractions;
using Zamin.Extensions.Translations.Abstractions;
using Zamin.Extensions.UsersManagement.Abstractions;
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
