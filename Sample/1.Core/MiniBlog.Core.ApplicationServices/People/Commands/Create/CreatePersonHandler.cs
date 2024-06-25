
using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Core.RequestResponse.Library.Common;
using DDD.Utilities.Library;
using FluentValidation;
using MiniBlog.Core.Contracts.People.Commands;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Core.RequestResponse.People.Commands.Create;

namespace MiniBlog.Core.ApplicationServices.People.Commands.Create;

public class CreatePersonHandler : CommandHandler<CreatePerson, int>
{
    private readonly IPersonCommandRepository repository;
    public CreatePersonHandler(UtilitiesServices utilitiesServices, IPersonCommandRepository repository) : base(utilitiesServices)
    {
        this.repository = repository;
    }

    public override async Task<CommandResult<int>> Handle(CreatePerson command)
    {
        Person person = new Person(command.Id, command.FirstName, command.LastName);
        //repository.Insert(person);
        //await repository.CommitAsync();
        throw new ValidationException("My Error");
        return await ResultAsync(person.Id,ApplicationServiceStatus.NotFound);
    }
}
