using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options): base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<StorageProduct> StorageProducts { get; set; }
    }
}
