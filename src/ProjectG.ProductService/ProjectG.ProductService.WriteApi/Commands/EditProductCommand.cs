namespace ProjectG.ProductService.WriteApi.Commands
{
    using System.IO;
    using System.Threading.Tasks;

    using ProjectG.ProductService.Infrastructure.Interfaces;
    using ProjectG.ProductService.WriteApi.DTO;
    using ProjectG.Core;
    using ProjectG.ProductService.Core.Models;

    public class EditProductCommand : ICommandHandler<ProductEditModel>
    {
        private readonly IProductRepository productRepository;

        public EditProductCommand(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task Execute(ProductEditModel commandData)
        {
            Product product = await this.productRepository.Get(commandData.Id);

            if (product == null)
            {
                throw new InvalidDataException($"Product with ID: {commandData.Id} is not found");
            }

            product.Name = commandData.Name;
            product.Description = commandData.Description;
            product.Count = commandData.Count;
            product.Price = commandData.Price;

            await this.productRepository.Update(product);
        }
    }
}