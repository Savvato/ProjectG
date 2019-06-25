namespace ProjectG.OrderService.Infrastructure.CustomerApi.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.OrderService.Infrastructure.CustomerApi.DTO;

    public interface ICustomerReadApiClient
    {
        Task<CustomerModel> Get(long customerId);
    }
}