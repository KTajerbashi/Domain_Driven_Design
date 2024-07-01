using FluentValidation;

namespace MiniBlog.Core.RequestResponse.Admins.Commands.Create;

public sealed class AdminCreateValidator : AbstractValidator<AdminCreateCommand>
{
    public AdminCreateValidator()
    {
        RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("عنوان خالی است");
        RuleFor(x => x.RoleId).NotNull().NotEmpty().WithMessage("نقش خالی است");
    }
}
