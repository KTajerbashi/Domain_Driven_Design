using DDD.Core.Data.Sql.Queries.Library;
using Microsoft.EntityFrameworkCore;

namespace MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext
{
    public class MiniBlogQueriesDbContext : BaseQueryDbContext
    {
        public MiniBlogQueriesDbContext(DbContextOptions options) : base(options)
        {
        }
    }
}
