using DDD.Core.RequestResponse.Library.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Core.RequestResponse.Advertisements.Queries.GetPagedFilter;

public sealed class AdvertisementGetPagedFilterQuery : PageQuery<PagedData<AdvertisementListItemQr>>
{
}
