using FoodDeliveryServer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDeliveryServer.Infrastructure
{
    public class FoodDeliveryContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        public DbSet<Ingradient> Ingradients { get; set; }

        public FoodDeliveryContext(DbContextOptions<FoodDeliveryContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure many-to-many
            modelBuilder.Entity<PizzaIngradients>().HasKey(gg => new { gg.PizzaId, gg.IngradientId });

        }
    }
}
