namespace ProjectG.ClientService.Infrastructure.OrderApi.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.OrderApi.DTO;

    public interface IOrderWriteApiClient
    {
        Task Create(OrderCreationRequestModel requestModel);
    }
}