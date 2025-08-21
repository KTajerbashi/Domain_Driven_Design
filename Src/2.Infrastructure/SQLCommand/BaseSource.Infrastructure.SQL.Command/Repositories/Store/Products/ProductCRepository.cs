using BaseSource.Core.Application.UseCases.Store.Products.Repositories;
using BaseSource.Core.Domain.Aggregates.Store.Products.Entities;

namespace BaseSource.Infrastructure.SQL.Command.Repositories.Store.Products;

public class ProductCRepository : CommandRepository<Product, long, CommandDatabaseContext>, IProductCRepository
{
    public ProductCRepository(CommandDatabaseContext context) : base(context)
    {
    }
}
