using DDD.Core.Domain.Library.Events;

namespace MiniBlog.Core.Domain.People.DomainEvents
{
    public record PersonCreated(int Id, Guid BusinessId, string FirstName, string LastName) : IDomainEvent;
    public record PersonNameChanged(int Id, string FirstName) : IDomainEvent;
}
