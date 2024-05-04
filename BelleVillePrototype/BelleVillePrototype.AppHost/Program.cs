var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.BelleVillePrototype_ApiService>("apiservice");

builder.AddProject<Projects.BelleVillePrototype_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();