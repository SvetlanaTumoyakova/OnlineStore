using Microsoft.EntityFrameworkCore;
using OnlineStore.Model;

namespace OnlineStore.Data
{
    public class OnlineStoreDBContext(DbContextOptions<OnlineStoreDBContext> options) :DbContext(options)
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
    }
}
