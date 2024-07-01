using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.Advertisments.Commands;
using MiniBlog.Core.Domain.Advertisements.Entities;
using MiniBlog.Core.RequestResponse.Courses.Commands.Update;

namespace MiniBlog.Core.ApplicationServices.Advertisements.Commands.UpdateCourse;

public class CourseUpdateHandler : CommandHandler<CourseUpdateCommand>
{
    private readonly IAdvertisementCommandRepository repository;
    public CourseUpdateHandler(UtilitiesServices utilitiesServices, IAdvertisementCommandRepository repository) : base(utilitiesServices)
    {
        this.repository = repository;
    }

    public override async Task<CommandResult> Handle(CourseUpdateCommand command)
    {
        var entity = UtilitiesServices.MapperFacade.Map<CourseUpdateCommand,Course>(command);
        repository.UpdateCourse(entity);
        await repository.CommitAsync();
        return await OkAsync();
    }
}
