var builder = WebApplication.CreateBuilder(args);

// Add dependency injection services here

var app = builder.Build();

// Configure the HTTP request pipeline here

app.Run();
