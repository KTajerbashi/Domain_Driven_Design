using Extensions.Translations.Abstractions;
using FluentValidation;

namespace MiniBlog.Core.RequestResponse.Admins.Commands.Update;

public sealed class AdminUpdateValidator : AbstractValidator<AdminUpdateCommand>
{
    public AdminUpdateValidator(ITranslator translator)
    {
    }
}
