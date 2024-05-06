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
        
        List<IdentityRole<Guid>> roles = new()
        {
            new IdentityRole<Guid> {Id = Guid.NewGuid(), Name = "Admin", NormalizedName = "ADMIN"},
            new IdentityRole<Guid> {Id = Guid.NewGuid(), Name = "User", NormalizedName = "USER"}
        };
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(roles);
    }
}