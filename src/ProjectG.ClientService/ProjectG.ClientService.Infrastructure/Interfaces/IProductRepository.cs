namespace ProjectG.ClientService.Infrastructure.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ProjectG.ClientService.Infrastructure.ProductApi.DTO;

    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> Get();

        Task<ProductModel> Get(long id);

        Task Create(ProductWriteModel product);

        Task Edit(long id, ProductWriteModel product);
    }
}