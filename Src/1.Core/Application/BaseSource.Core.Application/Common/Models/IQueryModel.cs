using BaseSource.Core.Domain.Common.ValueObjects;

namespace BaseSource.Core.Application.Common.Models;

public interface IQueryModel
{
}

public abstract class QueryModel : IQueryModel
{
    public long Id { get; set; }
    public EntityId EntityId { get; set; }
}
