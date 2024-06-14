using Basket.API.Basket.DeleteBasket;
using Basket.API.Basket.GetBasket;
using BuildingBlocks.Behaviors;
using Carter;
using FluentValidation;
using Marten;

var builder = WebApplication.CreateBuilder(args);

// Add services to container

/*
 * Carter - API Endpoints
 */
builder.Services.AddCarter(null, config =>
{
    config.WithModule<GetBasketEndpoint>();
    config.WithModule<DeleteBasketEndpoint>();
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

var app = builder.Build();

// Configure HTTP request pipeline

app.Run();
