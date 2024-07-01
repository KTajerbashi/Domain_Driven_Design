using DDD.Core.RequestResponse.Library.Commands;
using MiniBlog.Core.RequestResponse.Admins.Commands.Create;

namespace MiniBlog.Core.RequestResponse.Courses.Commands.Create;


public sealed class CourseCreateCommand : ICommand<long>
{
    public long AdvertisementId { get; set; }
    public long Id { get;  set; }
    public string Name { get;  set; }
    public int Length { get;  set; }
    public DateTime From { get;  set; }
    public DateTime To { get;  set; }

    public List<AdminCreateCommand> Admins { get; set; }
}
