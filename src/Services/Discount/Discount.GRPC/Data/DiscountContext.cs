using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.GRPC.Data
{
    public class DiscountContext : DbContext
    {
        public DbSet<Coupon> Coupons { get; set; } = default!;
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coupon>().HasData(
                new Coupon { Id = 1, ProductName = "IPhone X", Description = "IPhone Ten", Amount = 10 },
                new Coupon { Id = 2, ProductName = "IPhone XXX", Description = "IPhone Sexxxy", Amount = 69 }
                );
            base.OnModelCreating(modelBuilder);
        }

    }
}
