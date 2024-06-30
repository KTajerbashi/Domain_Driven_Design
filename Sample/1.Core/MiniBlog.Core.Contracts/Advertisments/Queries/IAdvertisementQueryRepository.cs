using DDD.Core.Contracts.Library.Data.Queries;
using DDD.Core.RequestResponse.Library.Queries;
using MiniBlog.Core.RequestResponse.Advertisments.Queries.GetById;
using MiniBlog.Core.RequestResponse.Advertisments.Queries.GetPagedFilter;
using MiniBlog.Core.RequestResponse.Advertisments.Queries.GetSelectList;

namespace MiniBlog.Core.Contracts.Advertisments.Queries;

public interface IAdvertisementQueryRepository
{
    Task<AdvertisementQr?> ExecuteAsync(AdvertisementGetByIdQuery query);

    Task<List<AdvertisementSelectItemQr>> ExecuteAsync(AdvertisementGetSelectListQuery query);

    Task<PagedData<AdvertisementListItemQr>> ExecuteAsync(AdvertisementGetPagedFilterQuery query);
}