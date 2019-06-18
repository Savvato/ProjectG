namespace ProjectG.ClientService.Infrastructure.ProductApi.Interfaces
{
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.ProductApi.DTO;

    public interface IProductWriteApiClient
    {
        Task Create(ProductWriteModel productModel);
    }
}