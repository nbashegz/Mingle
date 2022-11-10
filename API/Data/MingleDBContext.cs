using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MingleDBContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public MingleDBContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Photo>? Photos { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
        .ToTable("Users")
        .HasMany(ur => ur.UserRoles)
        .WithOne(ur => ur.User)
        .HasForeignKey(ur => ur.UserId)
        .IsRequired();

        builder.Entity<AppRole>()
        .ToTable("Roles")
            .HasMany(Ur => Ur.UserRoles)
            .WithOne(Ur => Ur.Role)
            .HasForeignKey(Ur => Ur.RoleId)
            .IsRequired();

        builder.Entity<IdentityUserClaim<int>>()
        .ToTable("UserClaims");

        builder.Entity<IdentityUserLogin<int>>()
        .ToTable("UserLogins");

        builder.Entity<IdentityUserToken<int>>()
        .ToTable("UserTokens");

        builder.Entity<IdentityRoleClaim<int>>()
        .ToTable("RoleClaims");

        builder.Entity<AppUserRole>()
        .ToTable("UserRoles");
    }
}