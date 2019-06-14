namespace ProjectG.ProductService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using ProjectG.Core;
    using ProjectG.ProductService.Core.Interfaces;
    using ProjectG.ProductService.Core.Models;
    using ProjectG.ProductService.WriteApi.DTO;
    using ProjectG.ProductService.WriteApi.Extensions;

    public class CreateProductCommand : ICommandHandler<ProductCreationModel>
    {
        private readonly IProductRepository productRepository;

        public CreateProductCommand(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task Execute(ProductCreationModel commandData)
        {
            Product product = commandData.ToProduct();
            await this.productRepository.Add(product);
        }
    }
}
