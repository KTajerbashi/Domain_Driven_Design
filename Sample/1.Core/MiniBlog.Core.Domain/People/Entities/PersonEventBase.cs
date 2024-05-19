using DDD.Core.Domain.Library.Entities;
using DDD.Core.Domain.Library.Exceptions;
using MiniBlog.Core.Domain.People.DomainEvents;
using MiniBlog.Core.Domain.People.ValueObjects;
using MiniBlog.Core.Domain.Resources;

namespace MiniBlog.Core.Domain.People.Entities
{
    public class PersonEventBase : AggregateRoot<int>
    {
        #region Properties
        public FirstName FirstName { get; set; }
        public LastName LastName { get; set; }
        #endregion
        public PersonEventBase(int id, string firstName, string lastName)
        {
            Apply(new PersonCreated(id, BusinessId.Value, firstName, lastName));
           
        }
        private void On(PersonCreated personCreated)
        {
            if (personCreated.Id < 1)
            {
                throw new InvalidEntityStateException(MessagePatterns.IdInvalidationMessage);
            }
            Id = personCreated.Id;
            FirstName = personCreated.FirstName;
            LastName = personCreated.LastName;
            BusinessId = personCreated.BusinessId;
        }
    }
}
