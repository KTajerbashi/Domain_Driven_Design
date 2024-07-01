using DDD.Core.RequestResponse.Library.Commands;

namespace MiniBlog.Core.RequestResponse.Advertisements.Commands.Update;

public sealed class AdvertisementUpdateCommand : ICommand
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int CityId { get; set; }
    public int Salary { get; set; }
    public bool IsRemote { get; set; }
    public List<CoursesUpdateCommand> Courses { get; set; }
}

