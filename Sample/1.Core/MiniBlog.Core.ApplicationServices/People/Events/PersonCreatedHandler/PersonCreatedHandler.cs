using DDD.Core.Contracts.Library.ApplicationServices.Events;
using Microsoft.Extensions.Logging;
using MiniBlog.Core.Contracts.People.Commands;
using MiniBlog.Core.Domain.People.DomainEvents;
using MiniBlog.Core.Domain.People.Entities;

namespace MiniBlog.Core.ApplicationServices.People.Events.PersonCreatedHandler
{
    public class PersonCreatedHandler : IDomainEventHandler<PersonCreated>
    {
        private readonly ILogger<PersonCreatedHandler> _logger;
        private readonly IPersonCommandRepository personCommandRepository;

        public PersonCreatedHandler(ILogger<PersonCreatedHandler> logger, IPersonCommandRepository personCommandRepository)
        {
            _logger = logger;
            this.personCommandRepository = personCommandRepository;
        }

        public async Task Handle(PersonCreated Event)
        {
            try
            {
                Person person = new Person(10,DateTime.Now.ToString(),DateTime.Now.ToString());

                await personCommandRepository.InsertAsync(person);
                await personCommandRepository.CommitAsync();
                
                _logger.LogInformation("Handeled {Event} in BlogCreatedHandler", Event.GetType().Name);
                
                await Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
