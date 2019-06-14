using Microsoft.EntityFrameworkCore;

using ProjectG.ProductService.Core.Models;

namespace ProjectG.ProductService.Infrastructure.Db
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }
    }
}