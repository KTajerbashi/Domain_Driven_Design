using DDD.Core.Domain.Library.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Zamin.Extensions.UsersManagement.Abstractions;

namespace DDD.Core.Data.Sql.Commands.Library.Extensions;
public static class AuditableShadowProperties
{
    /// <summary>
    /// 
    /// </summary>
    public static readonly Func<object, string> EFPropertyCreatedByUserId =
                                    entity => EF.Property<string>(entity, CreatedByUserId);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string CreatedByUserId = nameof(CreatedByUserId);

    /// <summary>
    /// 
    /// </summary>
    public static readonly Func<object, string> EFPropertyModifiedByUserId =
                                    entity => EF.Property<string>(entity, ModifiedByUserId);

    /// <summary>
    /// 
    /// </summary>
    public static readonly string ModifiedByUserId = nameof(ModifiedByUserId);

    /// <summary>
    /// 
    /// </summary>
    public static readonly Func<object, DateTime?> EFPropertyCreatedDateTime =
                                    entity => EF.Property<DateTime?>(entity, CreatedDateTime);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string CreatedDateTime = nameof(CreatedDateTime);
    
    /// <summary>
    /// 
    /// </summary>
    public static readonly Func<object, DateTime?> EFPropertyModifiedDateTime =
                                    entity => EF.Property<DateTime?>(entity, ModifiedDateTime);
    /// <summary>
    /// 
    /// </summary>
    public static readonly string ModifiedDateTime = nameof(ModifiedDateTime);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void AddAuditableShadowProperties(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(c => typeof(IAuditableEntity).IsAssignableFrom(c.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType)
                        .Property<string>(CreatedByUserId).HasMaxLength(50);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<string>(ModifiedByUserId).HasMaxLength(50);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime?>(CreatedDateTime);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime?>(ModifiedDateTime);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="changeTracker"></param>
    /// <param name="userInfoService"></param>
    public static void SetAuditableEntityPropertyValues(
        this ChangeTracker changeTracker,
        IUserInfoService userInfoService)
    {

        var userAgent = userInfoService.GetUserAgent();
        var userIp = userInfoService.GetUserIp();
        var now = DateTime.UtcNow;
        var userId = userInfoService.UserIdOrDefault();

        var modifiedEntries = changeTracker.Entries<IAuditableEntity>().Where(x => x.State == EntityState.Modified);
        foreach (var modifiedEntry in modifiedEntries)
        {
            modifiedEntry.Property(ModifiedDateTime).CurrentValue = now;
            modifiedEntry.Property(ModifiedByUserId).CurrentValue = userId;
        }

        var addedEntries = changeTracker.Entries<IAuditableEntity>().Where(x => x.State == EntityState.Added);
        foreach (var addedEntry in addedEntries)
        {
            addedEntry.Property(CreatedDateTime).CurrentValue = now;
            addedEntry.Property(CreatedByUserId).CurrentValue = userId;
        }
    }

}

