using DDD.Core.Domain.Library.Entities;
using DDD.Core.Domain.Library.Exceptions;
using MiniBlog.Core.Domain.People.DomainEvents;
using MiniBlog.Core.Domain.People.ValueObjects;
using MiniBlog.Core.Domain.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiniBlog.Core.Domain.People.Entities
{
    [Table("People",Schema ="Security"),Description("Users System")]
    public class Person : AggregateRoot<int>
    {
        #region Properties
        public FirstName FirstName { get; set; }
        public LastName LastName { get; set; }
        #endregion
        private Person()
        {
            
        }
        public Person(int id, string firstName, string lastName)
        {
            if (id < 1)
            {
                throw new InvalidEntityStateException(MessagePatterns.IdInvalidationMessage);
            }
            //Id = id;
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
