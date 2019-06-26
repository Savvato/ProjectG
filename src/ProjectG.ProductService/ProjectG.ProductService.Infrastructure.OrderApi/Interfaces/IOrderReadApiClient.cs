namespace ProjectG.ProductService.Infrastructure.OrderApi.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.ProductService.Infrastructure.OrderApi.DTO;

    public interface IOrderReadApiClient
    {
        Task<OrderModel> GetOrderById(long orderId);
    }
}