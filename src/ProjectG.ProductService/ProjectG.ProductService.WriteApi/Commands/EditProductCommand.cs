namespace ProjectG.ProductService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using ProjectG.ProductService.Infrastructure.Interfaces;
    using ProjectG.ProductService.WriteApi.DTO;
    using ProjectG.Core;

    public class EditProductCommand : ICommandHandler<ProductEditModel>
    {
        private readonly IProductRepository productRepository;

        public EditProductCommand(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task Execute(ProductEditModel commandData)
        {
            throw new System.NotImplementedException();
        }
    }
}