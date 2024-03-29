﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectG.OrderService.Core.Models;
using ProjectG.OrderService.Infrastructure.Db;

namespace ProjectG.OrderService.Infrastructure.Db.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    partial class OrderDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:Enum:order_status", "created,waiting_for_payment,paid,sent")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ProjectG.OrderService.Core.Models.Order", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CustomerId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<OrderStatus>("Status");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ProjectG.OrderService.Core.Models.OrderPosition", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<long>("OrderId");

                    b.Property<double>("Price");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("text");

                    b.Property<long>("ProductId");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderPositions");
                });

            modelBuilder.Entity("ProjectG.OrderService.Core.Models.OrderStatusDetails", b =>
                {
                    b.Property<long>("OrderId");

                    b.Property<bool>("AreProductsReserved");

                    b.Property<bool>("IsBasketCleared");

                    b.HasKey("OrderId");

                    b.ToTable("OrderStatusDetails");
                });

            modelBuilder.Entity("ProjectG.OrderService.Core.Models.OrderPosition", b =>
                {
                    b.HasOne("ProjectG.OrderService.Core.Models.Order", "Order")
                        .WithMany("OrderPositions")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ProjectG.OrderService.Core.Models.OrderStatusDetails", b =>
                {
                    b.HasOne("ProjectG.OrderService.Core.Models.Order", "Order")
                        .WithOne("StatusDetails")
                        .HasForeignKey("ProjectG.OrderService.Core.Models.OrderStatusDetails", "OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
