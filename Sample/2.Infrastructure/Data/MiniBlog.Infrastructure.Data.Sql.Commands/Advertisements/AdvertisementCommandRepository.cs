using DDD.Infra.Data.Sql.Commands.Library;
using MiniBlog.Core.Contracts.Advertisments.Commands;
using MiniBlog.Core.Domain.Advertisements.Entities;
using MiniBlog.Infrastructure.Data.Sql.Commands.DatabaseContext;

namespace MiniBlog.Infrastructure.Data.Sql.Commands.Advertisements;

public class AdvertisementCommandRepository :
        BaseCommandRepository<Advertisement, MiniBlogCommandsDbContext, long>,
        IAdvertisementCommandRepository
{
    public AdvertisementCommandRepository(MiniBlogCommandsDbContext dbContext) : base(dbContext)
    {
    }
}
