using Basket.API.Basket.GetBasket;
using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add services to container

/*
 * Carter - API Endpoints
 */
builder.Services.AddCarter(null, config =>
{
    config.WithModule<GetBasketEndpoint>();
});

var app = builder.Build();

// Configure HTTP request pipeline

app.Run();
