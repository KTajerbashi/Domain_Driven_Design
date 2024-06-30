//using DDD.Core.ApplicationServices.Library.Queries;
//using DDD.Core.RequestResponse.Library.Queries;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MiniBlog.Core.ApplicationServices.Advertisments.Queries.GetSelectList;

//public sealed class AdvertisementGetSelectListHandler : QueryHandler<AdvertisementGetSelectListQuery, List<AdvertisementSelectItemQr>>
//{
//    private readonly IAdvertisementQueryRepository _queryRepository;

//    public AdvertisementGetSelectListHandler(ZaminServices zaminServices, IAdvertisementQueryRepository queryRepository)
//        : base(zaminServices)
//    {
//        _queryRepository = queryRepository;
//    }

//    public override async Task<QueryResult<List<AdvertisementSelectItemQr>>> Handle(AdvertisementGetSelectListQuery query)
//    {
//        return Result(await _queryRepository.ExecuteAsync(query));
//    }
//}