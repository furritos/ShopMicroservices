using Ordering.Application;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (DI)

builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline

app.Run();
