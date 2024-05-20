using DDD.Core.Domain.Library.Entities;
using DDD.Core.Domain.Library.Exceptions;
using MiniBlog.Core.Domain.People.DomainEvents;
using MiniBlog.Core.Domain.People.ValueObjects;
using MiniBlog.Core.Domain.Resources;

namespace MiniBlog.Core.Domain.People.Entities
{
    public class Person : AggregateRoot<int>
    {
        #region Properties
        public FirstName FirstName { get; set; }
        public LastName LastName { get; set; }
        #endregion
        public Person(int id, string firstName, string lastName)
        {
            if (id < 1)
            {
                throw new InvalidEntityStateException(MessagePatterns.IdInvalidationMessage);
            }
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            AddEvent(new PersonCreated(id, BusinessId.Value, firstName, lastName));
        }
        public void ChangeFirstName(string firstName)
        {
            FirstName = firstName;
            AddEvent(new PersonNameChanged(Id, firstName));
        }
    }
}
