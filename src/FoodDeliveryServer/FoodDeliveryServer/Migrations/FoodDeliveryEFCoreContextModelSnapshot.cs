﻿// <auto-generated />
using System;
using FoodDeliveryServer.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodDeliveryServer.Migrations
{
    [DbContext(typeof(FoodDeliveryEFCoreContext))]
    partial class FoodDeliveryEFCoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FoodDeliveryServer.Entities.Ingradient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("Ingradients");
                });

            modelBuilder.Entity("FoodDeliveryServer.Entities.Pizza", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(5000);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("FoodDeliveryServer.Entities.PizzaIngradients", b =>
                {
                    b.Property<Guid>("PizzaId");

                    b.Property<Guid>("IngradientId");

                    b.HasKey("PizzaId", "IngradientId");

                    b.HasIndex("IngradientId");

                    b.ToTable("PizzaIngradients");
                });

            modelBuilder.Entity("FoodDeliveryServer.Entities.PizzaIngradients", b =>
                {
                    b.HasOne("FoodDeliveryServer.Entities.Ingradient", "Ingradient")
                        .WithMany("PizzaIngradients")
                        .HasForeignKey("IngradientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("FoodDeliveryServer.Entities.Pizza", "Pizza")
                        .WithMany("PizzaIngradients")
                        .HasForeignKey("PizzaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}