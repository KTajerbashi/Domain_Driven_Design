using BaseSource.Core.Domain.Aggregates.Store.Carts.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Customers.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Orders.Entities;
using BaseSource.Core.Domain.Aggregates.Store.Products.Entities;

namespace BaseSource.Infrastructure.SQL.Command.Persistence;

public class CommandDatabaseContext : BaseCommandDatabaseContext<CommandDatabaseContext>
{
    public CommandDatabaseContext(DbContextOptions<CommandDatabaseContext> options) : base(options)
    {
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
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
