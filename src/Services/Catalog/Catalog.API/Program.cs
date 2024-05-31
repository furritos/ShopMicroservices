using Carter;
using Catalog.API.Products.CreateProduct;

var builder = WebApplication.CreateBuilder(args);

// Add dependency injection services here
builder.Services.AddCarter(null, config =>
{
   config.WithModule<CreateProductEndpoint>();
});
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
