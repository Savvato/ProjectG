namespace ProjectG.OrderService.Infrastructure.Db
{
    using Microsoft.EntityFrameworkCore;

    using Npgsql;

    using ProjectG.OrderService.Core.Models;

    public class OrderDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderPosition> OrderPositions { get; set; }

        static OrderDbContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<OrderStatus>();
        }

        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ForNpgsqlHasEnum<OrderStatus>();
        }
    }
}