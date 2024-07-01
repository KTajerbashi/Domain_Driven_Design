using Extensions.Translations.Abstractions;
using FluentValidation;

namespace MiniBlog.Core.RequestResponse.Admins.Commands.Delete;

public sealed class AdminDeleteValidator : AbstractValidator<AdminDeleteCommand>
{
    public AdminDeleteValidator(ITranslator translator)
    {
    }
}
