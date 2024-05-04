using System.Runtime.CompilerServices;
using BelleVillePrototype.ApiService.Posts;
using Microsoft.EntityFrameworkCore;
using BelleVillePrototype.ApiService.Infrastructure;

namespace BelleVillePrototype.ApiService.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<PostModel> Posts { get; set; }
}