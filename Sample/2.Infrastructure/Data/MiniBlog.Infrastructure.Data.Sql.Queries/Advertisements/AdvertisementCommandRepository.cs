using DDD.Core.RequestResponse.Library.Queries;
using DDD.Infra.Data.Sql.Queries.Library;
using MiniBlog.Core.Contracts.Advertisments.Queries;
using MiniBlog.Core.Domain.Advertisements.Entities;
using MiniBlog.Core.RequestResponse.Advertisements.Queries.GetById;
using MiniBlog.Core.RequestResponse.Advertisements.Queries.GetPagedFilter;
using MiniBlog.Core.RequestResponse.Advertisements.Queries.GetSelectList;
using MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext;

namespace MiniBlog.Infrastructure.Data.Sql.Queries.Advertisements;

public sealed class AdvertisementCommandRepository : BaseQueryRepository<MiniBlogQueriesDbContext>, IAdvertisementQueryRepository
{
    public AdvertisementCommandRepository(MiniBlogQueriesDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<AdvertisementQr?> ExecuteAsync(AdvertisementGetByIdQuery query)
    {
        //return await _dbContext.Set<Advertisement>()
        //   .Where(Advertisement => Advertisement.Id == query.Id)
        //   .Select(Advertisement => (AdvertisementQr)Advertisement)
        //   .FirstOrDefaultAsync();
        throw new Exception();
    }

    public async Task<List<AdvertisementSelectItemQr>> ExecuteAsync(AdvertisementGetSelectListQuery query)
    {
        //return await _dbContext.Set<Advertisement>().Select(Advertisement => (AdvertisementSelectItemQr)Advertisement).ToListAsync();
        throw new Exception();
    }

    public async Task<PagedData<AdvertisementListItemQr>> ExecuteAsync(AdvertisementGetPagedFilterQuery query)
    {
        //var filter = _dbContext.Set<Advertisement>().AsQueryable();

        throw new Exception();
        //return await filter.ToPagedData(query, Advertisement => (AdvertisementListItemQr)Advertisement);
    }
}
