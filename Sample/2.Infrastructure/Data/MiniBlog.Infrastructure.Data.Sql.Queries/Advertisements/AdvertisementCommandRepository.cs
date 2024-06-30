using DDD.Infra.Data.Sql.Queries.Library;
using MiniBlog.Core.Contracts.Advertisments.Queries;
using MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniBlog.Infrastructure.Data.Sql.Queries.Advertisements;

public sealed class AdvertisementCommandRepository : BaseQueryRepository<MiniBlogQueriesDbContext>, IAdvertisementQueriesRepository
{
    public AdvertisementCommandRepository(MiniBlogQueriesDbContext dbContext) : base(dbContext)
    {
    }
}
