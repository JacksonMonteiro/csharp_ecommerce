using Ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data {
    public class EcommerceContext : DbContext {
        public EcommerceContext(DbContextOptions<EcommerceContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Promotion> Promotions { get; set; }

    }
}
