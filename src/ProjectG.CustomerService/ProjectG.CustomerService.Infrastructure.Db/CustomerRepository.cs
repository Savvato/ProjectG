namespace ProjectG.CustomerService.Infrastructure.Db
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using ProjectG.CustomerService.Core.Interfaces;
    using ProjectG.CustomerService.Core.Models;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext dbContext;

        public CustomerRepository(CustomerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Customer> Get()
        {
            return this.dbContext.Customers;
        }

        public async Task<Customer> Get(long id)
        {
            return await this.dbContext.Customers.FirstOrDefaultAsync(customer => customer.Id == id);
        }

        public async Task Add(Customer customer)
        {
            await this.dbContext.AddAsync(customer);
            await this.dbContext.SaveChangesAsync();
        }
    }
}