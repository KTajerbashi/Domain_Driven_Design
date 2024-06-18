using FluentValidation;

namespace MiniBlog.Core.RequestResponse.People.Queries.GetAllPerson;

public class GetAllPersonValidator : AbstractValidator<GetAllPersonModel>
{
    public GetAllPersonValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("");
    }
}
