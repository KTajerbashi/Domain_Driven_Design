using BaseSource.Infrastructure.SQL.Common.Identity.Entities;
using BaseSource.Infrastructure.SQL.Common.Persistence.Conversions;
using BaseSource.Infrastructure.SQL.Common.Persistence.Interceptors;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.Infrastructure.SQL.Common.Persistence;


public abstract class BaseDatabaseContext<TContext> : IdentityDbContext
    <UserIdentity, RoleIdentity, long, UserClaimIdentity, UserRoleIdentity, UserLoginIdentity, RoleClaimIdentity, UserTokenIdentity>
    where TContext : DbContext
{
    protected BaseDatabaseContext()
    {
    }

    protected BaseDatabaseContext(DbContextOptions<TContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(BaseDatabaseContext<TContext>).Assembly);
        base.OnModelCreating(builder);
        builder.AddShadowProperty();
        builder.ApplyIdentityConfiguration();

    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<EntityId>().HaveConversion<EntityIdConversion>();

    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }
    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return base.SaveChangesAsync(cancellationToken);
    }

}
