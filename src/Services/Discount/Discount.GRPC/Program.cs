using Discount.GRPC.Data;
using Discount.GRPC.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc().AddJsonTranscoding();

/*
 * Swagger for GRPC
 */
builder.Services.AddGrpcSwagger();
builder.Services.AddSwaggerGen();


/*
 * Inject SQLite Services
 */
builder.Services.AddDbContext<DiscountContext>(opts =>
    opts.UseSqlite(builder.Configuration.GetConnectionString("Database"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.

/*
 * Enable Swagger UI
 */

if (app.Environment.IsDevelopment()) { 
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
 * Use Auto-Migration extension created in Data folder
 */
app.UseMigration();

app.MapGrpcService<DiscountService>();

app.Run();
