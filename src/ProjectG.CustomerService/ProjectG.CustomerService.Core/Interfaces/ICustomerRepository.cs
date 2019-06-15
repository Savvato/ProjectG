namespace ProjectG.CustomerService.Core.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;

    using ProjectG.CustomerService.Core.Models;

    public interface ICustomerRepository
    {
        IQueryable<Customer> Get();

        Task<Customer> Get(long id);

        Task Add(Customer customer);
    }
}
