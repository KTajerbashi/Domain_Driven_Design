using BaseSource.Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace BaseSource.Infrastructure.SQL.Common.Persistence.Interceptors;

public class AddAuditDataInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        AddShadowProperties(eventData);
        return base.SavingChanges(eventData, result);
    }
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        AddShadowProperties(eventData);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    private static void AddShadowProperties(DbContextEventData eventData)
    {
        var changeTracker = eventData.Context?.ChangeTracker;
        var userInfoService = eventData.Context?.GetService<IUser>();
        changeTracker?.SetAuditableEntityPropertyValues(userInfoService!);
    }
}

public static class AuditableShadowExtensions
{
    public static ModelBuilder AddShadowProperty(this ModelBuilder modelBuilder)
    {
        //
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var builder = modelBuilder.Entity(entityType.ClrType);

            // Add shadow properties
            builder.Property<bool>("IsActive").HasDefaultValue(true);
            builder.Property<bool>("IsDeleted").HasDefaultValue(false);

            builder.Property<long>("CreatedByUserRoleId");
            builder.Property<long?>("UpdatedByUserRoleId");

            builder.Property<DateTime>("CreatedDate");
            builder.Property<DateTime?>("UpdatedDate");
        }

        //
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IAuditableEntity<long>).IsAssignableFrom(entityType.ClrType))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "e");

                var prop = Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant(AuditableShadowProperties.IsDeleted));

                var filter = Expression.Lambda(Expression.Equal(prop, Expression.Constant(false)), parameter);

                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
            }
        }
        return modelBuilder;
    }
}


public static class AuditableShadowProperties
{
    public static readonly Func<object, bool> EFPropertyIsActive = entity => EF.Property<bool>(entity, IsActive);
    public static readonly string IsActive = nameof(IsActive);

    public static readonly Func<object, bool> EFPropertyIsDeleted = entity => EF.Property<bool>(entity, IsDeleted);
    public static readonly string IsDeleted = nameof(IsDeleted);

    public static readonly Func<object, long> EFPropertyCreatedByUserRoleId = entity => EF.Property<long>(entity, CreatedByUserRoleId);
    public static readonly string CreatedByUserRoleId = nameof(CreatedByUserRoleId);

    public static readonly Func<object, long?> EFPropertyUpdatedByUserRoleId = entity => EF.Property<long?>(entity, UpdatedByUserRoleId);
    public static readonly string UpdatedByUserRoleId = nameof(UpdatedByUserRoleId);

    public static readonly Func<object, DateTime> EFPropertyCreatedDate = entity => EF.Property<DateTime>(entity, CreatedDate);
    public static readonly string CreatedDate = nameof(CreatedDate);

    public static readonly Func<object, DateTime?> EFPropertyUpdatedDate = entity => EF.Property<DateTime?>(entity, UpdatedDate);
    public static readonly string UpdatedDate = nameof(UpdatedDate);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static ModelBuilder AddAuditableShadowProperties<TId>(this ModelBuilder modelBuilder)
        where TId : struct,
              IComparable,
              IComparable<TId>,
              IConvertible,
              IEquatable<TId>,
              IFormattable
    {
        /// تمامی کلاس های که اینترفس 
        /// IAuditableEntity
        /// را دارند را بگیر و پراپرتی های مورد نظر را به آنها اضافه کن
        /// و این فقط فبلد دیتابیس ساخته میشود
        foreach (var entityType in modelBuilder.Model.GetEntityTypes().Where(c => typeof(IAuditableEntity<TId>).IsAssignableFrom(c.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType)
                        .Property<bool>(IsDeleted).IsRequired().HasDefaultValue(false);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<bool>(IsActive).IsRequired().HasDefaultValue(true);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<long>(CreatedByUserRoleId).HasMaxLength(50);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<long?>(UpdatedByUserRoleId).HasMaxLength(50);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>(CreatedDate);
            modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime?>(UpdatedDate);
        }

        return modelBuilder;
    }

    /// <summary>
    /// این متد فیلد های که ساخته میشود را مقدار دهی میکند
    /// </summary>
    /// <param name="changeTracker"></param>
    /// <param name="userInfoService"></param>
    public static void SetAuditableEntityPropertyValues(
        this ChangeTracker changeTracker,
        IUser user)
    {

        var userAgent = user.Agent;
        var userIp = user.Ip;
        var now = DateTime.Now;
        var userRoleId = user.UserRoleId;

        var modifiedEntries = changeTracker.Entries().Where(x => x.State == EntityState.Modified);
        foreach (var modifiedEntry in modifiedEntries)
        {
            modifiedEntry.Property(UpdatedDate).CurrentValue = now;
            modifiedEntry.Property(UpdatedByUserRoleId).CurrentValue = userRoleId;
        }

        var addedEntries = changeTracker.Entries().Where(x => x.State == EntityState.Added);
        foreach (var addedEntry in addedEntries)
        {
            addedEntry.Property(CreatedDate).CurrentValue = now;
            addedEntry.Property(CreatedByUserRoleId).CurrentValue = userRoleId;
            addedEntry.Property(IsActive).CurrentValue = true;
            addedEntry.Property(IsDeleted).CurrentValue = false;
        }
    }

}

