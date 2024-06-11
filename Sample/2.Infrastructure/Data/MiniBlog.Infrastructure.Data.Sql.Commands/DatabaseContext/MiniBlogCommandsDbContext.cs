using DDD.Infra.Data.Sql.Commands.Library;
using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Core.Domain.People.ValueObjects;
using MiniBlog.Infrastructure.Data.Sql.Commands.People;

namespace MiniBlog.Infrastructure.Data.Sql.Commands.DatabaseContext
{
    public class MiniBlogCommandsDbContext : BaseCommandDbContext
    {
        public MiniBlogCommandsDbContext(DbContextOptions<MiniBlogCommandsDbContext> options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<FirstName>().HaveConversion<FirstNameConversion>();
            configurationBuilder.Properties<LastName>().HaveConversion<LastNameConversion>();
        }
        public DbSet<Person> People { get; set; }
    }
}
