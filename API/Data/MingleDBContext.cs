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

    public DbSet<Photo>? Photos {get;set;}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppUser>()
        .HasMany(ur => ur.UserRoles)
        .WithOne(ur => ur.User)
        .HasForeignKey(ur => ur.UserId)
        .IsRequired();

        builder.Entity<AppRole>()
            .HasMany(Ur => Ur.UserRoles)
            .WithOne(Ur => Ur.Role)
            .HasForeignKey(Ur => Ur.UserId)
            .IsRequired();
    }
}