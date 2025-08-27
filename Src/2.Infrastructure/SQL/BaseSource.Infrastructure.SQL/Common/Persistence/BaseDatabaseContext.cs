using BaseSource.Core.Domain.Aggregates.Store.Orders.ValueObjects;
using BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;
using BaseSource.Core.Domain.ValueObjects.Common;
using BaseSource.Infrastructure.SQL.Command.Conversions.Store;
using BaseSource.Infrastructure.SQL.Common.Identity.Entities;
using BaseSource.Infrastructure.SQL.Common.Persistence.Conversions;
using BaseSource.Infrastructure.SQL.Common.Persistence.Interceptors;
using BaseSource.Infrastructure.SQL.Conversions;
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



    public virtual DbSet<RevokedToken> RevokedTokens => Set<RevokedToken>();

    protected BaseDatabaseContext(DbContextOptions<TContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyIdentityConfiguration();
        builder
            .AddAuditableShadowProperties()
            .AddSoftDeleteQueryFilter();

    }
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<EntityId>().HaveConversion<EntityIdConversion>();
        configurationBuilder.Properties<Title>().HaveConversion<TitleConversion>();
        configurationBuilder.Properties<Description>().HaveConversion<DescriptionConversion>();
        configurationBuilder.Properties<Address>().HaveConversion<AddressConversion>();
        configurationBuilder.Properties<ProductWeight>().HaveConversion<ProductWeightConversion>();
        configurationBuilder.Properties<ProductCategory>().HaveConversion<ProductCategoryConversion>();
        configurationBuilder.Properties<ProductDimensions>().HaveConversion<ProductDimensionsConversion>();

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
