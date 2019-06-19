namespace ProjectG.BasketService.Infrastructure.Db
{
    using Microsoft.EntityFrameworkCore;

    using ProjectG.BasketService.Core.Models;

    public class BasketDbContext : DbContext
    {
        public DbSet<BasketPosition> BasketPositions { get; set; }

        public BasketDbContext(DbContextOptions<BasketDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BasketPosition>()
                .HasIndex(position => new { position.CustomerId, position.ProductId })
                .IsUnique(true);
        }
    }
}
