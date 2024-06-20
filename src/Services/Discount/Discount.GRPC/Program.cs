using Discount.GRPC.Data;
using Discount.GRPC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddGrpc();

/*
 * Inject SQLite Services
 */
builder.Services.AddDbContext<DiscountContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("Database"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.

/*
 * Use Auto-Migration extension created in Data folder
 */
app.UseMigration();

app.MapGrpcService<DiscountService>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
