using System.Runtime.CompilerServices;
using BelleVillePrototype.ApiService.Posts;
using Microsoft.EntityFrameworkCore;
using BelleVillePrototype.ApiService.Infrastructure;

namespace BelleVillePrototype.ApiService.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<PostModel> Posts { get; set; }
}

public static class Extensions
{
    public static void CreateDbIfNotExists(this IHost host)
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApplicationDbContext>();
        try
        {
            context.Database.EnsureCreated();
            DbInitializer.Initialize(context);

        }
        catch (Exception ex)
        {
        }
    }
}


public static class DbInitializer
{
    public static void Initialize(ApplicationDbContext context)
    {
        if (context.Posts.Any())
            return;

        var posts = new List<PostModel>
        {
            new PostModel { Author = "John Doe", Title = "First Post" },
            new PostModel { Author = "Other man", Title = "Some guy" },
        };

        context.AddRange(posts);

        context.SaveChanges();
    }
}