var builder = DistributedApplication.CreateBuilder(args);

var bankAApi =  builder.AddProject<Projects.BankA_WebApi>("BankA-WebApi");

builder.AddNpmApp("BankA-WebApp", "../BankA.WebApp")
    .WithReference(bankAApi)
    .WaitFor(bankAApi)
    .WithHttpEndpoint(targetPort: 49873, env: "PORT")
    .WithExternalHttpEndpoints();

builder.Build().Run();
