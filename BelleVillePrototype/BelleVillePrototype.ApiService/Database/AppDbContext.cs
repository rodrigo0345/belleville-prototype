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
}