var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Basket_API>("basket-api");

builder.AddProject<Projects.Discount_GRPC>("discount-grpc");

builder.Build().Run();
