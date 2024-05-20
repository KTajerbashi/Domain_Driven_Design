using DDD.Core.ApplicationServices.Library.Commands;
using DDD.Core.RequestResponse.Library.Commands;
using DDD.Utilities.Library;
using MiniBlog.Core.Contracts.People.Commands;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Core.RequestResponse.People.Commands.ChangeName;

namespace MiniBlog.Core.ApplicationServices.People.Commands.ChangeName
{
    public class ChangePersonNameHandler : CommandHandler<ChangePersonName, int>
    {
        private readonly IPersonCommandRepository repository;

        public ChangePersonNameHandler(UtilitiesServices utilitiesServices, IPersonCommandRepository repository) : base(utilitiesServices)
        {
            this.repository = repository;
        }

        public override async Task<CommandResult<int>> Handle(ChangePersonName command)
        {
            Person person = repository.GetGraph(command.Id);
            person.ChangeFirstName(command.FirstName);
            await repository.CommitAsync();
            return await OkAsync(person.Id);

        }
    }
}
