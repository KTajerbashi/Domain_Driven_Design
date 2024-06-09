using FluentValidation;
using Extensions.Translations.Abstractions;

namespace MiniBlog.Core.RequestResponse.People.Commands.ChangeName
{
    public class ChangePersonNameValidator: AbstractValidator<ChangePersonName>
    {
        public ChangePersonNameValidator(ITranslator translator)
        {
            RuleFor(c => c.FirstName)
                .NotNull().WithMessage(translator["Required", "FirstName"])
                .MinimumLength(2).WithMessage(translator["MinimumLength", "FirstName", "2"])
                .MaximumLength(100).WithMessage(translator["MaximumLength", "FirstName", "50"]);

        }
    }
}
