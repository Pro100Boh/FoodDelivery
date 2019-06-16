using FoodDeliveryServer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDeliveryServer.Infrastructure
{
    public class FoodDeliveryContext : IdentityDbContext<User>
    {
        public DbSet<Pizza> Pizzas { get; set; }

        public DbSet<Ingradient> Ingradients { get; set; }

        public DbSet<Drink> Drinks { get; set; }

        public DbSet<Dessert> Desserts { get; set; }

        public DbSet<Order> Orders { get; set; }

        public FoodDeliveryContext(DbContextOptions<FoodDeliveryContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many
            modelBuilder.Entity<PizzaIngradients>().HasKey(gg => new { gg.PizzaId, gg.IngradientId });

        }
    }
}
