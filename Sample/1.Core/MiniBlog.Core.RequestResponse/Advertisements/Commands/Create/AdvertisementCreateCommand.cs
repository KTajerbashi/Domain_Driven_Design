using DDD.Core.RequestResponse.Library.Commands;
using MiniBlog.Core.RequestResponse.Courses.Commands.Create;

namespace MiniBlog.Core.RequestResponse.Advertisements.Commands.Create;

public sealed class AdvertisementCreateCommand : ICommand<long>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CityId { get; set; }
    public int Salary { get; set; }
    public bool IsRemote { get; set; }
    public List<CourseCreateCommand> Courses { get; set; }

}
