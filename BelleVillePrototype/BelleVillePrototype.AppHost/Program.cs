var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");
var postgresSql = builder.AddPostgres("main").WithPgAdmin();

var apiService = builder.AddProject<Projects.BelleVillePrototype_ApiService>("api-service")
    .WithReference(cache).WithReference(postgresSql);

builder.AddProject<Projects.BelleVillePrototype_Web>("web-frontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();