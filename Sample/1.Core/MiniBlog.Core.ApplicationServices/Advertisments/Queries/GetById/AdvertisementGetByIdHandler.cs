using DDD.Core.ApplicationServices.Library.Queries;
using DDD.Core.RequestResponse.Library.Queries;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.Advertisments.Queries;
using MiniBlog.Core.RequestResponse.Advertisments.Queries.GetById;

namespace MiniBlog.Core.ApplicationServices.Advertisments.Queries.GetById;

public sealed class AdvertisementGetByIdHandler : QueryHandler<AdvertisementGetByIdQuery, AdvertisementQr?>
{
    private readonly IAdvertisementQueryRepository _queryRepository;

    public AdvertisementGetByIdHandler(UtilitiesServices utilitiesServices, IAdvertisementQueryRepository queryRepository)
        : base(utilitiesServices)
    {
        _queryRepository = queryRepository;
    }

    public override async Task<QueryResult<AdvertisementQr?>> Handle(AdvertisementGetByIdQuery query)
    {
        return Result(await _queryRepository.ExecuteAsync(query));
    }
}