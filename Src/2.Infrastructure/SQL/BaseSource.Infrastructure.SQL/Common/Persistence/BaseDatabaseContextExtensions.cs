using BaseSource.Infrastructure.SQL.Common.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace BaseSource.Infrastructure.SQL.Common.Persistence;

public static class BaseDatabaseContextExtensions
{
    public static ModelBuilder ApplyIdentityConfiguration(this ModelBuilder builder)
    {
        builder.Entity<UserIdentity>().ToTable("Users", "Identity");

        builder.Entity<UserClaimIdentity>().ToTable("UserClaims", "Identity");

        builder.Entity<UserLoginIdentity>().ToTable("UserLogins", "Identity");

        builder.Entity<UserRoleIdentity>().ToTable("UserRoles", "Identity");

        builder.Entity<UserTokenIdentity>().ToTable("UserTokens", "Identity");

        builder.Entity<RoleIdentity>().ToTable("Roles", "Identity");

        builder.Entity<RoleClaimIdentity>().ToTable("RoleClaims", "Identity");
        return builder;
    }
}
