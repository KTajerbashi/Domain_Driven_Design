using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MiniBlog.Infrastructure.Data.Sql.Commands.DatabaseContext
{
    public class MiniBlogCommandsDbContextFactory : IDesignTimeDbContextFactory<MiniBlogCommandsDbContext>
    {
        public MiniBlogCommandsDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MiniBlogCommandsDbContext>();
            optionsBuilder.UseSqlServer("Server =TAJERBASHI; Database=MiniBlogDb;User Id = sa;Password=123123; MultipleActiveResultSets=true; Encrypt = false");

            return new MiniBlogCommandsDbContext(optionsBuilder.Options);
        }
    }
}
