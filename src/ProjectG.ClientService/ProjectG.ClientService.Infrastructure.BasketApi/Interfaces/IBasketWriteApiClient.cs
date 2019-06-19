namespace ProjectG.ClientService.Infrastructure.BasketApi.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.BasketApi.DTO;

    public interface IBasketWriteApiClient
    {
        Task Create(BasketPositionWriteModel basketPosition);
    }
}