namespace ProjectG.OrderService.Infrastructure.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.OrderService.Infrastructure.CustomerApi.DTO;

    public interface ICustomerRepository
    {
        Task<CustomerModel> Get(long customerId);
    }
}