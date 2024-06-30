using DDD.Core.RequestResponse.Library.Commands;

namespace MiniBlog.Core.RequestResponse.Advertisments.Commands.Update;

public sealed class AdvertisementUpdateCommand : ICommand<long>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CityId { get; set; }
    public int Salary { get; set; }
    public bool IsRemote { get; set; }
}
