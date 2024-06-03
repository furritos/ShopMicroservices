using Carter;
using Catalog.API.Products.CreateProduct;
using Marten;

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
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database"));
}).UseLightweightSessions();
// --------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline here
app.MapCarter();
// --------------------------------------
app.Run();
