using BuildingBlocks.Behaviors;
using Carter;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetProductByCategory;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.UpdateProduct;
using FluentValidation;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add dependency injection services here

/*
 * Carter - API Endpoints
 */
builder.Services.AddCarter(null, config =>
{
    config.WithModule<CreateProductEndpoint>();
    config.WithModule<GetProductsEndpoint>();
    config.WithModule<GetProductByIdEndpoint>();
    config.WithModule<GetProductByCategoryEndpoint>();
    config.WithModule<UpdateProductEndpoint>();
    config.WithModule<DeleteProductEndpoint>();
});

/*
 * MediatR - Command/Query Abstraction Layer
 */
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);

/*
 * Marten - Database repository; JSON documents in PostgreSQL
 */
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

// --------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline here
app.MapCarter();
// --------------------------------------
app.Run();
