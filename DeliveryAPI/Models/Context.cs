using Microsoft.EntityFrameworkCore;
using DeliveryAPI.Models.DTO;

namespace DeliveryAPI.Models
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

        public DbSet<Delivery> Deliveries { get; set; }
    }
}
