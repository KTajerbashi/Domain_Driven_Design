using Extensions.Translations.Abstractions;
using FluentValidation;

namespace MiniBlog.Core.RequestResponse.Advertisements.Commands.Create;

public sealed class AdvertisementCreateValidator : AbstractValidator<AdvertisementCreateCommand>
{
    public AdvertisementCreateValidator(ITranslator translator)
    {
        RuleFor(x => x.CityId).GreaterThan(0).WithMessage(translator["خالی نباشد"]);
    }
}
