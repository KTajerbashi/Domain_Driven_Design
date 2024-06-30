using FluentValidation;

namespace MiniBlog.Core.RequestResponse.Advertisements.Commands.UnPublishe;

public sealed class AdvertisementUnPublishValidator : AbstractValidator<AdvertisementUnPublishCommand>
{
    public AdvertisementUnPublishValidator()
    {

    }
}