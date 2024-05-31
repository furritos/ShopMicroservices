using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add dependency injection services here
builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline here
app.MapCarter();

app.Run();
