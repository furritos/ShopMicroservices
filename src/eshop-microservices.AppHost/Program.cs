var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Basket_API>("basket-api");

builder.Build().Run();
