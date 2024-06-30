using DDD.Core.Domain.Library.Events;

namespace MiniBlog.Core.Domain.Advertisements.DomainEvents;

public sealed record AdvertisementCreated(Guid BusinessId, string Title, string Description, int Salary, int CityId, bool IsRemote) : IDomainEvent;
