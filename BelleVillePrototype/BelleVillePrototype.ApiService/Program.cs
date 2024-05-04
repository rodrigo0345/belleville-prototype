using BelleVillePrototype.ApiService.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add logger
builder.Services.AddLogging();

// Add swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "BelleVillePrototype.ApiService", Version = "v1" });
});

// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddHttpLogging(options => { });
builder.Services.AddTransient<ILogger>(
    provider => provider.GetRequiredService<ILoggerFactory>().CreateLogger("BelleVillePrototype.ApiService"));

// Add database context.
/*
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlite("Data Source=app.db");
});
*/
builder.AddNpgsqlDbContext<ApplicationDbContext>("main");


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseHttpLogging();

app.CreateDbIfNotExists();

if (app.Environment.IsDevelopment())
{
    using var serviceScope = app.Services.CreateScope();
    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await dbContext.Database.MigrateAsync();
    
    
    // Map swagger api
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BelleVillePrototype.ApiService v1"));
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
        .ToArray();
    return forecast;
});

app.MapControllers();
app.MapDefaultEndpoints();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}