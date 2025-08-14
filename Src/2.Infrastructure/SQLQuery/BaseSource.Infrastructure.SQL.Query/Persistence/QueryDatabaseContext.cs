using BaseSource.Infrastructure.SQL.Query.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.Infrastructure.SQL.Query.Persistence;

public class QueryDatabaseContext : BaseQueryDatabaseContext<QueryDatabaseContext>
{
    public QueryDatabaseContext(DbContextOptions<QueryDatabaseContext> options) : base(options)
    {
    }
}