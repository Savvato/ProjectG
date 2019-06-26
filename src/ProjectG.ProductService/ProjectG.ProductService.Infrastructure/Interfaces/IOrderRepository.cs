namespace ProjectG.ProductService.Infrastructure.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.ProductService.Infrastructure.OrderApi.DTO;

    public interface IOrderRepository
    {
        Task<OrderModel> Get(long id);
    }
}