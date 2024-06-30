using DDD.Core.RequestResponse.Library.Commands;

namespace MiniBlog.Core.RequestResponse.Advertisements.Commands.Create;

public sealed class AdvertisementCoursesCreateCommand : ICommand<long>
{
    public string Name { get; set; }
    public int Length { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}
