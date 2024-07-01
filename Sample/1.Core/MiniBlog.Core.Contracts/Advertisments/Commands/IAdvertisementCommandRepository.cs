using DDD.Core.Contracts.Library.Data.Commands;
using MiniBlog.Core.Domain.Advertisements.Entities;

namespace MiniBlog.Core.Contracts.Advertisments.Commands;

public interface IAdvertisementCommandRepository : ICommandRepository<Advertisement, long>
{
    long CreateAdmin(Admin entity);
    long CreateCourse(Course entity);
    Course UpdateCourse(Course entity);
    bool DeleteCourse(Course entity);
}
