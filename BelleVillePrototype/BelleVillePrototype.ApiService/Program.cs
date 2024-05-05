using BelleVillePrototype.ApiService.Infrastructure;
using Carter;
using Carter.ResponseNegotiators.Newtonsoft;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.Services.AddCarter(configurator: c =>
{
    c.WithResponseNegotiator<NewtonsoftJsonResponseNegotiator>();
});

// Add logger
builder.Services.AddLogging();

// Add swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "BelleVillePrototype.ApiService", Version = "v1" });
});


// Add services to the container.
builder.Services.AddProblemDetails();
builder.Services.AddControllers();
builder.Services.AddHttpLogging(options => { });
builder.Services.AddTransient<ILogger>(
    provider => provider.GetRequiredService<ILoggerFactory>().CreateLogger("BelleVillePrototype.ApiService"));


builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("BelleVilleDb"));
});
/*
builder.AddNpgsqlDbContext<ApplicationDbContext>("main");
*/

builder.Services.AddMediatR(config =>
    config.RegisterServicesFromAssembly(typeof(Program).Assembly));


var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseExceptionHandler();
app.UseHttpLogging();

if (app.Environment.IsDevelopment())
{
    try
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }
    catch (Exception e)
    {
        app.Logger.LogWarning(e, "An error occurred while migrating the database.");
    }
    
    
    // Map swagger api
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BelleVillePrototype.ApiService v1"));
}

app.UseRouting();
app.MapCarter();
app.MapControllers();
app.MapDefaultEndpoints();
app.Run();