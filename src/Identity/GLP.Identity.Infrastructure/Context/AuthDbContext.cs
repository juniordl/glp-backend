using GLP.Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GLP.Identity.Infrastructure.Context;

public sealed class AuthDbContext :
    IdentityDbContext<ApplicationUser, ApplicationRole, Guid,
        IdentityUserClaim<Guid>, IdentityUserRole<Guid>,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
        IdentityUserToken<Guid>>
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);
        b.HasDefaultSchema("auth");

        b.Entity<ApplicationUser>(e =>
        {
            e.ToTable("Users");
            e.Property(x => x.FullName).HasMaxLength(150);
            e.Property(x => x.IsActive).HasDefaultValue(true);
        });

        b.Entity<ApplicationRole>().ToTable("Roles");
        b.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
        b.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
        b.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
        b.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
        b.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");

        // Índices útiles
        b.Entity<ApplicationUser>().HasIndex(u => u.UserName).HasDatabaseName("IX_Users_UserName").IsUnique();
    }
}