using BaseSource.Core.Application.UseCases.Store.Orders.Models;
using BaseSource.Core.Application.UseCases.Store.Products.Models;
using BaseSource.Core.Application.UseCases.Store.ShoppingCarts.Models;
using BaseSource.Infrastructure.SQL.Query.Common.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BaseSource.Infrastructure.SQL.Query.Persistence;

public class QueryDatabaseContext : BaseQueryDatabaseContext<QueryDatabaseContext>
{
    public QueryDatabaseContext(DbContextOptions<QueryDatabaseContext> options) : base(options)
    {
    }
    // DbSets for Read operations (Query-specific models)
    public DbSet<ProductDto> ProductDtos => Set<ProductDto>();
    public DbSet<OrderDto> OrderDtos => Set<OrderDto>();
    public DbSet<ShoppingCartDto> ShoppingCartDtos => Set<ShoppingCartDto>();
    public DbSet<ProductSummaryDto> ProductSummaries => Set<ProductSummaryDto>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply all query configurations
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(QueryDatabaseContext).Assembly,
            type => type.Namespace?.Contains("Query.Configurations") == true);

        // Configure query models as keyless since they're read-only views
        modelBuilder.Entity<ProductDto>().HasNoKey().ToView("vw_Products");
        modelBuilder.Entity<OrderDto>().HasNoKey().ToView("vw_Orders");
        modelBuilder.Entity<ShoppingCartDto>().HasNoKey().ToView("vw_ShoppingCarts");
        modelBuilder.Entity<ProductSummaryDto>().HasNoKey().ToView("vw_ProductSummaries");
    }
}