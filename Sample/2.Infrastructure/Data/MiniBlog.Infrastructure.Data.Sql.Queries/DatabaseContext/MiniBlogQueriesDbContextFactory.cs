using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MiniBlog.Infrastructure.Data.Sql.Queries.DatabaseContext;

public class MiniBlogQueriesDbContextFactory : IDesignTimeDbContextFactory<MiniBlogQueriesDbContext>
{
    public MiniBlogQueriesDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<MiniBlogQueriesDbContext>();
        optionsBuilder.UseSqlServer("Server =TAJERBASHI; Database=MiniBlogDb;User Id = sa;Password=123123; MultipleActiveResultSets=true; Encrypt = false");
        //optionsBuilder.UseSqlServer("Server =172.20.1.20\\DEV; Database=MiniBlogDb;User Id = sa;Password=soft157703ware; MultipleActiveResultSets=true; Encrypt = false");

        return new MiniBlogQueriesDbContext(optionsBuilder.Options);
    }
}
