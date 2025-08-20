using BaseSource.Core.Domain.Common.ValueObjects;

namespace BaseSource.Core.Application.Common.Models;

public interface IBaseDTO
{
    EntityId EntityId { get; set; }
}

public abstract class BaseDTO : IBaseDTO
{
    public EntityId EntityId { get; set; }

}