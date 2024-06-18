using FluentValidation;

namespace MiniBlog.Core.RequestResponse.People.Queries.GetPersonById;

public class GetPersonByIdValidator : AbstractValidator<GetPersonByIdModel>
{
    public GetPersonByIdValidator()
    {
        RuleFor(x => x.PersonId).GreaterThan(0);
    }
}
