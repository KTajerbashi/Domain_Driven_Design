using DDD.Infra.Data.Sql.Commands.Library;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.Advertisments.Commands;
using MiniBlog.Core.Domain.Advertisements.Entities;
using MiniBlog.Infrastructure.Data.Sql.Commands.DatabaseContext;

namespace MiniBlog.Infrastructure.Data.Sql.Commands.Advertisements;

public class AdvertisementCommandRepository :
        BaseCommandRepository<Advertisement, MiniBlogCommandsDbContext, long>,
        IAdvertisementCommandRepository
{
    private readonly UtilitiesServices _services;
    public AdvertisementCommandRepository(MiniBlogCommandsDbContext dbContext, UtilitiesServices services) : base(dbContext)
    {
        _services = services;
    }

    public long CreateAdmin(Admin entity)
    {
        _dbContext.Admins.Add(entity);
        _dbContext.SaveChanges();
        return entity.Id;
    }

    public long CreateCourse(Course entity)
    {
        _dbContext.Courses.Add(entity);
        _dbContext.SaveChanges();
        return entity.Id;
    }

    public bool DeleteCourse(Course entity)
    {
        _dbContext.Courses.Remove(entity);
        return true;
    }

    public Course UpdateCourse(Course entity)
    {
        _dbContext.Courses.Update(entity);
        return entity;
    }
}
