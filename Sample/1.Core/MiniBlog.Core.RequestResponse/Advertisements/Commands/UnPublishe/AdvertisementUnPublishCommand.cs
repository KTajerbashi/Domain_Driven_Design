using DDD.Core.RequestResponse.Library.Commands;

namespace MiniBlog.Core.RequestResponse.Advertisements.Commands.UnPublishe;

public sealed class AdvertisementUnPublishCommand : ICommand
{
    public long Id { get; set; }
}
