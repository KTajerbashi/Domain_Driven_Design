//using DDD.Core.ApplicationServices.Library.Queries;
//using DDD.Core.RequestResponse.Library.Queries;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MiniBlog.Core.ApplicationServices.Advertisments.Queries.GetPagedFilter;
//public sealed class AdvertisementGetPagedFilterHandler : QueryHandler<AdvertisementGetPagedFilterQuery, PagedData<AdvertisementListItemQr>>
//{
//    private readonly IAdvertisementQueryRepository _queryRepository;

//    public AdvertisementGetPagedFilterHandler(ZaminServices zaminServices, IAdvertisementQueryRepository queryRepository)
//        : base(zaminServices)
//    {
//        _queryRepository = queryRepository;
//    }

//    public override async Task<QueryResult<PagedData<AdvertisementListItemQr>>> Handle(AdvertisementGetPagedFilterQuery query)
//    {
//        return Result(await _queryRepository.ExecuteAsync(query));
//    }
//}