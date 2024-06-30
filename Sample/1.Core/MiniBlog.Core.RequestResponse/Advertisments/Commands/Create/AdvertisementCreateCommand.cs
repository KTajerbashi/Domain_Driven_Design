using DDD.Core.RequestResponse.Library.Commands;
using Extensions.Translations.Abstractions;
using FluentValidation;

namespace MiniBlog.Core.RequestResponse.Advertisments.Commands.Create;

public sealed class AdvertisementCreateCommand : ICommand<long>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int CityId { get; set; }
    public int Salary { get; set; }
    public bool IsRemote { get; set; }
}
public sealed class AdvertisementCreateValidator : AbstractValidator<AdvertisementCreateCommand>
{
    public AdvertisementCreateValidator(ITranslator translator)
    {
        RuleFor(x => x.CityId).GreaterThan(0).WithMessage(translator["خالی نباشد"]);
    }
}
