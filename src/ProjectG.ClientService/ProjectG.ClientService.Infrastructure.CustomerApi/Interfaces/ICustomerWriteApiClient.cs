namespace ProjectG.ClientService.Infrastructure.CustomerApi.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;

    public interface ICustomerWriteApiClient
    {
        Task Create(CustomerWriteModel customerModel);
    }
}