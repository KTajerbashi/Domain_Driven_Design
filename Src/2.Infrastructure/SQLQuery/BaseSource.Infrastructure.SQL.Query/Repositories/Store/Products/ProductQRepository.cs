using BaseSource.Core.Application.UseCases.Store.Products.Repositories;
using BaseSource.Core.Domain.Aggregates.Store.Products.Entities;


namespace BaseSource.Infrastructure.SQL.Query.Repositories.Store.Products;

public class ProductQRepository : QueryRepository<Product, long, QueryDatabaseContext>, IProductQRepository
{
    public ProductQRepository(QueryDatabaseContext context) : base(context)
    {
    }
}
