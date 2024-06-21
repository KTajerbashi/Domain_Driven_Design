using Extensions.Events.Abstractions;
using Extensions.Events.Outbox.Extensions.Events.Outbox.Dal.EF;
using Microsoft.EntityFrameworkCore;
using MiniBlog.Core.Domain.People.Entities;
using MiniBlog.Core.Domain.People.ValueObjects;
using MiniBlog.Infrastructure.Data.Sql.Commands.People.ValueConversions;

namespace MiniBlog.Infrastructure.Data.Sql.Commands.DatabaseContext
{
    /// <summary>
    /// بدون رویداد ها
    /// </summary>
    //public class MiniBlogCommandsDbContext : BaseCommandDbContext

    /// <summary>
    /// 
    /// </summary>
    public class MiniBlogCommandsDbContext : BaseOutboxCommandDbContext
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
        public override DbSet<OutBoxEventItem> OutBoxEventItems { get => base.OutBoxEventItems; set => base.OutBoxEventItems = value; }
    }
}
