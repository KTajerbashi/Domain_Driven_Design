using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.Advertisments.Commands;
using MiniBlog.Core.Domain.Advertisements.Entities;
using MiniBlog.Core.Domain.Advertisements.Parameters;
using MiniBlog.Core.Domain.Advertisements.Parameters.Courses;
using MiniBlog.Core.RequestResponse.Courses.Commands.Create;

namespace MiniBlog.Core.ApplicationServices.Advertisements.Commands.CreateCourse;

public class CourseCreateHandler : CommandHandler<CourseCreateCommand, long>
{
    private readonly IAdvertisementCommandRepository repository;
    public CourseCreateHandler(UtilitiesServices utilitiesServices, IAdvertisementCommandRepository repository) : base(utilitiesServices)
    {
        this.repository = repository;
    }

    public override async Task<CommandResult<long>> Handle(CourseCreateCommand command)
    {
        try
        {
            repository.BeginTransaction();
            var parameters = UtilitiesServices.MapperFacade.Map<CourseCreateCommand,CourseCreateParameter>(command);
            var entity = new Course(parameters);
            repository.CreateCourse(entity);
            throw new Exception("11235456478789");
            repository.CommitTransaction();
            return await OkAsync(entity.Id);
        }
        catch (Exception)
        {
            repository.RollbackTransaction();
            throw;
        }
    }
}
