using DDD.Core.RequestResponse.Library.Commands;

namespace MiniBlog.Core.RequestResponse.Courses.Commands.Update;


public sealed class CourseUpdateCommand : ICommand
{
    public long Id { get; set; }
    public string Name { get; set; }
    public int Length { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}
