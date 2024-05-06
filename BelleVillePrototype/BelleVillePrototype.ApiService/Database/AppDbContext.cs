using System.Collections.Immutable;
using System.Runtime.CompilerServices;
using BelleVillePrototype.ApiService.Entities;
using Microsoft.EntityFrameworkCore;
using BelleVillePrototype.ApiService.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BelleVillePrototype.ApiService.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<UserEntity, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<PostEntity> Posts { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        var roles = Enum.GetValues(typeof(UserEntityRole))
            .Cast<UserEntityRole>()
            .Select(r => new IdentityRole<Guid>
            {
                Id = Guid.NewGuid(),
                Name = r.ToString(),
                NormalizedName = r.ToString().ToUpper()
            })
            .ToList();

        // Seed roles
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(roles);

        // Add admin user
        var adminUser = new UserEntity
        {
            Id = Guid.NewGuid(),
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        PasswordHasher<UserEntity> passwordHasher = new();
        adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin");
        modelBuilder.Entity<UserEntity>().HasData(adminUser);

        // Add role to admin user
        var adminRoleId = roles.First(x => x.Name == nameof(UserEntityRole.Admin)).Id;
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        {
            RoleId = adminRoleId,
            UserId = adminUser.Id
        });
    }
}