using DDD.Core.RequestResponse.Library.Commands;

namespace MiniBlog.Core.RequestResponse.Courses.Commands.Delete;


public sealed class CourseDeleteCommand : ICommand<long>
{
    public long Id { get; set; }
}
