using DDD.Core.Domain.Library.Events;

namespace MiniBlog.Core.Domain.Advertisements.DomainEvents;

public record AdvertisementCreated(Guid BusinessId, string Title, string Description, int Salary, int CityId, bool IsRemote) : IDomainEvent;
public record AdvertisementUpdated(Guid BusinessId) : IDomainEvent;
public record AdvertisementDeleted(Guid BusinessId) : IDomainEvent;
