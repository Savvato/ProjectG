namespace ProjectG.CustomerService.Infrastructure.Db
{
    using Microsoft.EntityFrameworkCore;

    using ProjectG.CustomerService.Core.Models;

    public class CustomerDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }
    }
}