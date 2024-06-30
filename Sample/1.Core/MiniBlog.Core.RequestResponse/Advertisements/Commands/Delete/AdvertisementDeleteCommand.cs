using DDD.Core.RequestResponse.Library.Commands;

namespace MiniBlog.Core.RequestResponse.Advertisements.Commands.Delete;

public sealed class AdvertisementDeleteCommand : ICommand
{
    public long Id { get; set; }
}
