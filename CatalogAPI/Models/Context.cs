using Microsoft.EntityFrameworkCore;

namespace CatalogAPI.Models
{
    public class Context : DbContext
    {
        public Context()
        { }
        public Context(DbContextOptions<Context> options): base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }
        public DbSet<Product> Products { get; set; }
    }
}
