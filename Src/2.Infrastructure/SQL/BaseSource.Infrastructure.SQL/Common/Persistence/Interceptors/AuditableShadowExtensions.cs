using BaseSource.Core.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq.Expressions;

namespace BaseSource.Infrastructure.SQL.Common.Persistence.Interceptors;


public static class AuditableShadowProperties
{
    // Property names
    public const string IsActive = nameof(IsActive);
    public const string IsDeleted = nameof(IsDeleted);
    public const string CreatedByUserRoleId = nameof(CreatedByUserRoleId);
    public const string UpdatedByUserRoleId = nameof(UpdatedByUserRoleId);
    public const string CreatedDate = nameof(CreatedDate);
    public const string UpdatedDate = nameof(UpdatedDate);

    // EF Property accessors
    public static readonly Func<object, bool> EFPropertyIsActive =
        entity => EF.Property<bool>(entity, IsActive);

    public static readonly Func<object, bool> EFPropertyIsDeleted =
        entity => EF.Property<bool>(entity, IsDeleted);

    public static readonly Func<object, long> EFPropertyCreatedByUserRoleId =
        entity => EF.Property<long>(entity, CreatedByUserRoleId);

    public static readonly Func<object, long?> EFPropertyUpdatedByUserRoleId =
        entity => EF.Property<long?>(entity, UpdatedByUserRoleId);

    public static readonly Func<object, DateTime> EFPropertyCreatedDate =
        entity => EF.Property<DateTime>(entity, CreatedDate);

    public static readonly Func<object, DateTime?> EFPropertyUpdatedDate =
        entity => EF.Property<DateTime?>(entity, UpdatedDate);
}

public static class ModelBuilderExtensions
{
    /// <summary>
    /// Adds auditable shadow properties to entities implementing IAuditableEntity
    /// </summary>
    public static ModelBuilder AddAuditableShadowProperties(this ModelBuilder modelBuilder)
    {
        if (modelBuilder == null)
            throw new ArgumentNullException(nameof(modelBuilder));

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (IsAuditableEntity(entityType.ClrType))
            {
                ConfigureAuditableEntity(modelBuilder, entityType.ClrType);
            }
        }

        return modelBuilder;
    }

    /// <summary>
    /// Adds global query filter for soft delete
    /// </summary>
    public static ModelBuilder AddSoftDeleteQueryFilter(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (IsAuditableEntity(entityType.ClrType))
            {
                AddSoftDeleteFilter(modelBuilder, entityType.ClrType);
            }
        }

        return modelBuilder;
    }

    private static bool IsAuditableEntity(Type entityType)
    {
        return entityType.GetInterfaces()
            .Any(i => i.IsGenericType &&
                     i.GetGenericTypeDefinition() == typeof(IAuditableEntity<>));
    }

    private static void ConfigureAuditableEntity(ModelBuilder modelBuilder, Type entityType)
    {
        var entityBuilder = modelBuilder.Entity(entityType);

        entityBuilder.Property<bool>(AuditableShadowProperties.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        entityBuilder.Property<bool>(AuditableShadowProperties.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        entityBuilder.Property<long>(AuditableShadowProperties.CreatedByUserRoleId)
            .IsRequired();

        entityBuilder.Property<long?>(AuditableShadowProperties.UpdatedByUserRoleId);

        entityBuilder.Property<DateTime>(AuditableShadowProperties.CreatedDate)
            .IsRequired();

        entityBuilder.Property<DateTime?>(AuditableShadowProperties.UpdatedDate);
    }

    private static void AddSoftDeleteFilter(ModelBuilder modelBuilder, Type entityType)
    {
        //var parameter = Expression.Parameter(entityType, "e");
        //var propertyAccess = Expression.PropertyOrField(
        //    Expression.Constant(null, entityType),
        //    AuditableShadowProperties.IsDeleted);

        //var filterExpression = Expression.Lambda(
        //    Expression.Equal(propertyAccess, Expression.Constant(false)),
        //    parameter);

        //modelBuilder.Entity(entityType).HasQueryFilter(filterExpression);
    }
}

public static class ChangeTrackerExtensions
{
    /// <summary>
    /// Sets auditable entity property values during SaveChanges
    /// </summary>
    public static void SetAuditableEntityPropertyValues(this ChangeTracker changeTracker, IUserSystem user)
    {
        if (changeTracker == null)
            throw new ArgumentNullException(nameof(changeTracker));

        if (user == null)
            throw new ArgumentNullException(nameof(user));

        var utcNow = DateTime.UtcNow;
        var userRoleId = user.UserRoleId;

        ProcessModifiedEntities(changeTracker, utcNow, userRoleId);
        ProcessAddedEntities(changeTracker, utcNow, userRoleId);
    }

    private static void ProcessModifiedEntities(ChangeTracker changeTracker, DateTime utcNow, long userRoleId)
    {
        var modifiedEntries = changeTracker.Entries()
            .Where(entry => entry.State == EntityState.Modified &&
                           IsAuditableEntity(entry.Entity.GetType()));

        foreach (var entry in modifiedEntries)
        {
            entry.Property(AuditableShadowProperties.UpdatedDate).CurrentValue = utcNow;
            entry.Property(AuditableShadowProperties.UpdatedByUserRoleId).CurrentValue = userRoleId;
        }
    }

    private static void ProcessAddedEntities(ChangeTracker changeTracker, DateTime utcNow, long userRoleId)
    {
        var addedEntries = changeTracker.Entries()
            .Where(entry => entry.State == EntityState.Added &&
                           IsAuditableEntity(entry.Entity.GetType()));

        foreach (var entry in addedEntries)
        {
            entry.Property(AuditableShadowProperties.CreatedDate).CurrentValue = utcNow;
            entry.Property(AuditableShadowProperties.CreatedByUserRoleId).CurrentValue = userRoleId;
            entry.Property(AuditableShadowProperties.IsActive).CurrentValue = true;
            entry.Property(AuditableShadowProperties.IsDeleted).CurrentValue = false;
        }
    }

    private static bool IsAuditableEntity(Type entityType)
    {
        return entityType.GetInterfaces()
            .Any(i => i.IsGenericType &&
                     i.GetGenericTypeDefinition() == typeof(IAuditableEntity<>));
    }
}
public class AuditableEntityInterceptor : SaveChangesInterceptor
{
    public AuditableEntityInterceptor()
    {
    }

    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        var userInfoService = eventData.Context?.GetService<IUserSystem>();
        SetAuditableProperties(eventData.Context, userInfoService);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var userInfoService = eventData.Context?.GetService<IUserSystem>();
        SetAuditableProperties(eventData.Context, userInfoService);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void SetAuditableProperties(DbContext? context,IUserSystem userSystem)
    {
        if (context == null) return;

        context.ChangeTracker.SetAuditableEntityPropertyValues(userSystem);
    }
}
#region Comment
//public static class AuditableShadowProperties
//{
//    public static readonly string IsActive = nameof(IsActive);
//    public static readonly string IsDeleted = nameof(IsDeleted);
//    public static readonly string CreatedByUserRoleId = nameof(CreatedByUserRoleId);
//    public static readonly string UpdatedByUserRoleId = nameof(UpdatedByUserRoleId);
//    public static readonly string CreatedDate = nameof(CreatedDate);
//    public static readonly string UpdatedDate = nameof(UpdatedDate);


//    public static readonly Func<object, bool> EFPropertyIsActive = entity => EF.Property<bool>(entity, IsActive);
//    public static readonly Func<object, bool> EFPropertyIsDeleted = entity => EF.Property<bool>(entity, IsDeleted);
//    public static readonly Func<object, long> EFPropertyCreatedByUserRoleId = entity => EF.Property<long>(entity, CreatedByUserRoleId);
//    public static readonly Func<object, long?> EFPropertyUpdatedByUserRoleId = entity => EF.Property<long?>(entity, UpdatedByUserRoleId);
//    public static readonly Func<object, DateTime> EFPropertyCreatedDate = entity => EF.Property<DateTime>(entity, CreatedDate);
//    public static readonly Func<object, DateTime?> EFPropertyUpdatedDate = entity => EF.Property<DateTime?>(entity, UpdatedDate);

//    /// <summary>
//    /// Add shadow properties to entities implementing IAuditableEntity
//    /// </summary>
//    public static ModelBuilder AddAuditableShadowProperties<TId>(this ModelBuilder modelBuilder)
//        where TId : struct,
//              IComparable,
//              IComparable<TId>,
//              IConvertible,
//              IEquatable<TId>,
//              IFormattable
//    {
//        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
//                     .Where(c => typeof(IAuditableEntity<TId>).IsAssignableFrom(c.ClrType)))
//        {
//            modelBuilder.Entity(entityType.ClrType)
//                .Property<bool>(IsDeleted).IsRequired().HasDefaultValue(false);

//            modelBuilder.Entity(entityType.ClrType)
//                .Property<bool>(IsActive).IsRequired().HasDefaultValue(true);

//            modelBuilder.Entity(entityType.ClrType)
//                .Property<long>(CreatedByUserRoleId);

//            modelBuilder.Entity(entityType.ClrType)
//                .Property<long?>(UpdatedByUserRoleId);

//            modelBuilder.Entity(entityType.ClrType)
//                .Property<DateTime>(CreatedDate);

//            modelBuilder.Entity(entityType.ClrType)
//                .Property<DateTime?>(UpdatedDate);
//        }

//        return modelBuilder;
//    }

//    /// <summary>
//    /// Fill shadow properties during SaveChanges
//    /// </summary>
//    public static void SetAuditableEntityPropertyValues(this ChangeTracker changeTracker, IUserSystem user)
//    {
//        var now = DateTime.UtcNow; // 🔄 Better to use UTC for consistency
//        var userRoleId = user.UserRoleId;

//        foreach (var entry in changeTracker.Entries().Where(x => x.State == EntityState.Modified))
//        {
//            entry.Property(UpdatedDate).CurrentValue = now;
//            entry.Property(UpdatedByUserRoleId).CurrentValue = userRoleId;
//        }

//        foreach (var entry in changeTracker.Entries().Where(x => x.State == EntityState.Added))
//        {
//            entry.Property(CreatedDate).CurrentValue = now;
//            entry.Property(CreatedByUserRoleId).CurrentValue = userRoleId;
//            entry.Property(IsActive).CurrentValue = true;
//            entry.Property(IsDeleted).CurrentValue = false;
//        }
//    }
//}

//public class AddAuditDataInterceptor : SaveChangesInterceptor
//{
//    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
//    {
//        AddShadowProperties(eventData);
//        return base.SavingChanges(eventData, result);
//    }
//    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
//    {
//        AddShadowProperties(eventData);
//        return base.SavingChangesAsync(eventData, result, cancellationToken);
//    }
//    private static void AddShadowProperties(DbContextEventData eventData)
//    {
//        var changeTracker = eventData.Context?.ChangeTracker;
//        var userInfoService = eventData.Context?.GetService<IUserSystem>();
//        changeTracker?.SetAuditableEntityPropertyValues(userInfoService!);
//    }
//}

//public static class AuditableShadowExtensions
//{
//    public static ModelBuilder AddShadowProperty(this ModelBuilder modelBuilder)
//    {
//        //
//        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
//        {
//            var builder = modelBuilder.Entity(entityType.ClrType);

//            // Add shadow properties
//            builder.Property<bool>("IsActive").HasDefaultValue(true);
//            builder.Property<bool>("IsDeleted").HasDefaultValue(false);

//            builder.Property<long>("CreatedByUserRoleId");
//            builder.Property<long?>("UpdatedByUserRoleId");

//            builder.Property<DateTime>("CreatedDate").HasDefaultValue(DateTime.Now);
//            builder.Property<DateTime?>("UpdatedDate");
//        }

//        //
//        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
//        {
//            if (typeof(IAuditableEntity<long>).IsAssignableFrom(entityType.ClrType))
//            {
//                var parameter = Expression.Parameter(entityType.ClrType, "e");

//                var prop = Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant(AuditableShadowProperties.IsDeleted));

//                var filter = Expression.Lambda(Expression.Equal(prop, Expression.Constant(false)), parameter);

//                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
//            }
//        }
//        return modelBuilder;
//    }
//}


//public static class AuditableShadowProperties
//{
//    public static readonly string IsActive = nameof(IsActive);
//    public static readonly string IsDeleted = nameof(IsDeleted);
//    public static readonly string CreatedByUserRoleId = nameof(CreatedByUserRoleId);
//    public static readonly string UpdatedByUserRoleId = nameof(UpdatedByUserRoleId);
//    public static readonly string CreatedDate = nameof(CreatedDate);
//    public static readonly string UpdatedDate = nameof(UpdatedDate);


//    public static readonly Func<object, bool> EFPropertyIsActive = entity => EF.Property<bool>(entity, IsActive);
//    public static readonly Func<object, bool> EFPropertyIsDeleted = entity => EF.Property<bool>(entity, IsDeleted);
//    public static readonly Func<object, long> EFPropertyCreatedByUserRoleId = entity => EF.Property<long>(entity, CreatedByUserRoleId);
//    public static readonly Func<object, long?> EFPropertyUpdatedByUserRoleId = entity => EF.Property<long?>(entity, UpdatedByUserRoleId);
//    public static readonly Func<object, DateTime> EFPropertyCreatedDate = entity => EF.Property<DateTime>(entity, CreatedDate);
//    public static readonly Func<object, DateTime?> EFPropertyUpdatedDate = entity => EF.Property<DateTime?>(entity, UpdatedDate);

//    /// <summary>
//    /// Add shadow properties to entities implementing IAuditableEntity
//    /// </summary>
//    public static ModelBuilder AddAuditableShadowProperties<TId>(this ModelBuilder modelBuilder)
//        where TId : struct,
//              IComparable,
//              IComparable<TId>,
//              IConvertible,
//              IEquatable<TId>,
//              IFormattable
//    {
//        foreach (var entityType in modelBuilder.Model.GetEntityTypes()
//                     .Where(c => typeof(IAuditableEntity<TId>).IsAssignableFrom(c.ClrType)))
//        {
//            modelBuilder.Entity(entityType.ClrType)
//                .Property<bool>(IsDeleted).IsRequired().HasDefaultValue(false);

//            modelBuilder.Entity(entityType.ClrType)
//                .Property<bool>(IsActive).IsRequired().HasDefaultValue(true);

//            modelBuilder.Entity(entityType.ClrType)
//                .Property<long>(CreatedByUserRoleId);

//            modelBuilder.Entity(entityType.ClrType)
//                .Property<long?>(UpdatedByUserRoleId);

//            modelBuilder.Entity(entityType.ClrType)
//                .Property<DateTime>(CreatedDate);

//            modelBuilder.Entity(entityType.ClrType)
//                .Property<DateTime?>(UpdatedDate);
//        }

//        return modelBuilder;
//    }

//    /// <summary>
//    /// Fill shadow properties during SaveChanges
//    /// </summary>
//    public static void SetAuditableEntityPropertyValues(this ChangeTracker changeTracker, IUserSystem user)
//    {
//        var now = DateTime.UtcNow; // 🔄 Better to use UTC for consistency
//        var userRoleId = user.UserRoleId;

//        foreach (var entry in changeTracker.Entries().Where(x => x.State == EntityState.Modified))
//        {
//            entry.Property(UpdatedDate).CurrentValue = now;
//            entry.Property(UpdatedByUserRoleId).CurrentValue = userRoleId;
//        }

//        foreach (var entry in changeTracker.Entries().Where(x => x.State == EntityState.Added))
//        {
//            entry.Property(CreatedDate).CurrentValue = now;
//            entry.Property(CreatedByUserRoleId).CurrentValue = userRoleId;
//            entry.Property(IsActive).CurrentValue = true;
//            entry.Property(IsDeleted).CurrentValue = false;
//        }
//    }
//}

//public class AddAuditDataInterceptor : SaveChangesInterceptor
//{
//    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
//    {
//        AddShadowProperties(eventData);
//        return base.SavingChanges(eventData, result);
//    }
//    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
//    {
//        AddShadowProperties(eventData);
//        return base.SavingChangesAsync(eventData, result, cancellationToken);
//    }
//    private static void AddShadowProperties(DbContextEventData eventData)
//    {
//        var changeTracker = eventData.Context?.ChangeTracker;
//        var userInfoService = eventData.Context?.GetService<IUserSystem>();
//        changeTracker?.SetAuditableEntityPropertyValues(userInfoService!);
//    }
//}

//public static class AuditableShadowExtensions
//{
//    public static ModelBuilder AddShadowProperty(this ModelBuilder modelBuilder)
//    {
//        //
//        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
//        {
//            var builder = modelBuilder.Entity(entityType.ClrType);

//            // Add shadow properties
//            builder.Property<bool>("IsActive").HasDefaultValue(true);
//            builder.Property<bool>("IsDeleted").HasDefaultValue(false);

//            builder.Property<long>("CreatedByUserRoleId");
//            builder.Property<long?>("UpdatedByUserRoleId");

//            builder.Property<DateTime>("CreatedDate").HasDefaultValue(DateTime.Now);
//            builder.Property<DateTime?>("UpdatedDate");
//        }

//        //
//        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
//        {
//            if (typeof(IAuditableEntity<long>).IsAssignableFrom(entityType.ClrType))
//            {
//                var parameter = Expression.Parameter(entityType.ClrType, "e");

//                var prop = Expression.Call(typeof(EF), nameof(EF.Property), new[] { typeof(bool) }, parameter, Expression.Constant(AuditableShadowProperties.IsDeleted));

//                var filter = Expression.Lambda(Expression.Equal(prop, Expression.Constant(false)), parameter);

//                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(filter);
//            }
//        }
//        return modelBuilder;
//    }
//}
#endregion

