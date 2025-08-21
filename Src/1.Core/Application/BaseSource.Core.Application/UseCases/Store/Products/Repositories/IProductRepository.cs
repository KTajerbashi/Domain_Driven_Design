using BaseSource.Core.Application.Common.Repositories;
using BaseSource.Core.Domain.Aggregates.Store.Products.Entities;

namespace BaseSource.Core.Application.UseCases.Store.Products.Repositories;

public interface IProductCRepository : ICommandRepository<Product, long>
{
}
public interface IProductQRepository : IQueryRepository<Product, long>
{
}
