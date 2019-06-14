namespace ProjectG.CustomerService.Infrastructure.Db
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectG.CustomerService.Core.Interfaces;
    using ProjectG.CustomerService.Core.Models;

    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext dbContext;

        public CustomerRepository(CustomerDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IQueryable<Customer> GetQuery()
        {
            return this.dbContext.Customers;
        }

        public async Task Add(Customer customer)
        {
            await this.dbContext.AddAsync(customer);
            await this.dbContext.SaveChangesAsync();
        }
    }
}