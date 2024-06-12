using System.Collections.Generic;
using System.Threading.Tasks;

namespace Extensions.ChangeDataLog.Abstractions;
public interface IEntityChangeInterceptorItemRepository
{
    public void Save(List<EntityChangeInterceptorItem> entityChangeInterceptorItems);
    public Task SaveAsync(List<EntityChangeInterceptorItem> entityChangeInterceptorItems);
}
