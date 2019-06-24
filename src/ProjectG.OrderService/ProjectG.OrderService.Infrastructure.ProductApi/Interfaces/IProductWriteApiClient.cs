namespace ProjectG.OrderService.Infrastructure.ProductApi.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.OrderService.Infrastructure.ProductApi.DTO;

    public interface IProductWriteApiClient
    {
        Task Edit(ProductModel productModel);
    }
}