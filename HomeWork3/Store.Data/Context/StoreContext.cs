using Microsoft.EntityFrameworkCore;
using Store.Data.Entites;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Store.Data.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options) { }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

    }
}
