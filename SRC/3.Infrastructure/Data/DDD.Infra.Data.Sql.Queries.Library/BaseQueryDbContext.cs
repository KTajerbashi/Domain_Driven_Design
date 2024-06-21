using Microsoft.EntityFrameworkCore;

namespace DDD.Infra.Data.Sql.Queries.Library;

/// <summary>
/// 
/// </summary>
public abstract class BaseQueryDbContext : DbContext
{
    public BaseQueryDbContext(DbContextOptions options) : base(options) { }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// نباید نوشتن کار کند
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public override int SaveChanges()
    {
        throw new NotSupportedException();
    }
    
    /// <summary>
    /// نباید نوشتن کار کند
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        throw new NotSupportedException();

    }


    /// <summary>
    /// نباید نوشتن کار کند
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();

    }
    

    /// <summary>
    /// نباید نوشتن کار کند
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException();

    }
}

