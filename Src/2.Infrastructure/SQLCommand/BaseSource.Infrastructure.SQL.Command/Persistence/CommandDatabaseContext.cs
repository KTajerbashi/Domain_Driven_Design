using BaseSource.Core.Domain.Aggregates.Store.Carts.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Customers;
using BaseSource.Core.Domain.Aggregates.Store.Customers.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Orders.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Orders.ValueObjects;
using BaseSource.Core.Domain.Aggregates.Store.Products.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Products.ValueObjects;
using BaseSource.Core.Domain.ValueObjects.Common;
using BaseSource.Infrastructure.SQL.Command.Conversions.Store;
using BaseSource.Infrastructure.SQL.Conversions;

namespace BaseSource.Infrastructure.SQL.Command.Persistence;

public class CommandDatabaseContext : BaseCommandDatabaseContext<CommandDatabaseContext>
{
    public CommandDatabaseContext(DbContextOptions<CommandDatabaseContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<Title>().HaveConversion<TitleConversion>();
        configurationBuilder.Properties<Description>().HaveConversion<DescriptionConversion>();
        configurationBuilder.Properties<Address>().HaveConversion<AddressConversion>();
        configurationBuilder.Properties<ProductWeight>().HaveConversion<ProductWeightConversion>();
        configurationBuilder.Properties<ProductCategory>().HaveConversion<ProductCategoryConversion>();
        configurationBuilder.Properties<ProductDimensions>().HaveConversion<ProductDimensionsConversion>();
    }


    // DbSets for Write operations
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();
    public DbSet<Product> Products => Set<Product>();
    public DbSet<ProductDetail> ProductDetails => Set<ProductDetail>();
    public DbSet<ProductImage> ProductImages => Set<ProductImage>();
    public DbSet<ProductComment> ProductComments => Set<ProductComment>();
    public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<OrderStatusHistory> OrderStatusHistory => Set<OrderStatusHistory>();
}
