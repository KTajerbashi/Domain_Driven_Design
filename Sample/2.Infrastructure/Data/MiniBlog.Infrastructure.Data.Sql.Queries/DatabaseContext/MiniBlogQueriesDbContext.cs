using DDD.Infra.Data.Sql.Queries.Library;
using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Domain.People.Entities;

namespace MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext
{
    public class MiniBlogQueriesDbContext : BaseQueryDbContext
    {
        public MiniBlogQueriesDbContext(DbContextOptions<MiniBlogQueriesDbContext> options) : base(options)
        {
        }
        public DbSet<Person> People { get; set; }
    }
}
