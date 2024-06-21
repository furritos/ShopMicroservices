using Basket.API.Basket.DeleteBasket;
using Basket.API.Basket.GetBasket;
using Basket.API.Basket.StoreBasket;
using Basket.API.Data;
using Basket.API.Models;
using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Carter;
using Discount.GRPC;
using FluentValidation;
using HealthChecks.UI.Client;
using Marten;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

// Add services to container

// ###### ==== APPLICATION SERVICES ==== #####

/*
 * Swagger - API UI Frontend
 */
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
 * Carter - API Endpoints
 */
builder.Services.AddCarter(null, config =>
{
    config.WithModule<GetBasketEndpoint>();
    config.WithModule<DeleteBasketEndpoint>();
    config.WithModule<StoreBasketEndpoint>();
});

/*
 * MediatR - Command/Query Abstraction Layer
 */
var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
// ###### ==== APPLICATION SERVICES ==== #####


// ###### ==== DATA SERVICES ==== #####

/*
 * Marten - Database repository; JSON documents in PostgreSQL
 */
var dbConnection = builder.Configuration.GetConnectionString("Database")!;
builder.Services.AddMarten(opts =>
{
    opts.Connection(dbConnection);
    /*
     * Since there's no "standard" GUID/ID field, Marten provides two ways to provide one:
     * https://martendb.io/documents/identity.html
     */
    opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);

}).UseLightweightSessions();

/*
 * IBasketRepositry - Repository Pattern Service
 */
builder.Services.AddScoped<IBasketRepository, BasketRepository>();

/*
 * Decorating using Scrutor library
 */
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

/*
 * Stack Exchange Redis Cache Connection Details
 */
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
});
// ###### ==== DATA SERVICES ==== #####



// ###### ==== GRPC SERVICES ==== #####
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GRPCSettings:DiscountURL"]!);
})
    .ConfigurePrimaryHttpMessageHandler(() =>
    {
        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        return handler;
    });
// ###### ==== GRPC SERVICES ==== #####



// ###### ==== CROSS-CUTTING SERVICES ==== #####
/*
 * Add custom exception handler
 */
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

/*
 * Registering health checks
 */
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

// --------------------------------------------------------------
// ###### ==== CROSS-CUTTING SERVICES ==== #####

var app = builder.Build();

/*
 * Enable Swagger UI
 */

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure the HTTP request pipeline here
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
