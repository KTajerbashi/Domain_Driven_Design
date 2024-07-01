using DDD.Core.ApplicationServices.Library.Queries;
using DDD.Core.RequestResponse.Library.Queries;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.Advertisments.Queries;
using MiniBlog.Core.RequestResponse.Admins.Queries.GetById;
using MiniBlog.Core.RequestResponse.Advertisements.Queries.GetById;
using MiniBlog.Core.RequestResponse.Courses.Queries.GetById;

namespace MiniBlog.Core.ApplicationServices.Advertisements.Queries.GetById;

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