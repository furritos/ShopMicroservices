using Carter;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add dependency injection services here
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
// --------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline here
app.MapCarter();
// --------------------------------------
app.Run();
