namespace ProjectG.BasketService.Api.Commands
{
    using System.IO;
    using System.Threading.Tasks;

    using ProjectG.BasketService.Api.DTO;
    using ProjectG.BasketService.Api.Extensions;
    using ProjectG.BasketService.Core.Models;
    using ProjectG.BasketService.Infrastructure.Interfaces;
    using ProjectG.BasketService.Infrastructure.ProductApi.Models;
    using ProjectG.Core;

    public class CreateBasketPositionCommand : ICommandHandler<BasketPositionCreationModel>
    {
        private readonly IBasketRepository basketRepository;
        private readonly IProductRepository productRepository;

        public CreateBasketPositionCommand(
            IBasketRepository basketRepository, 
            IProductRepository productRepository)
        {
            this.basketRepository = basketRepository;
            this.productRepository = productRepository;
        }

        public async Task Execute(BasketPositionCreationModel commandData)
        {
            BasketPosition basketPosition = commandData.ToBasketPosition();

            if (await this.basketRepository.Exists(basketPosition))
            {
                throw new InvalidDataException("This basket position has already added");
            }

            ProductModel product = await this.productRepository.Get(basketPosition.ProductId);

            basketPosition.ProductName = product.Name;
            basketPosition.ProductDescription = product.Description;

            await this.basketRepository.Add(basketPosition);
        }
    }
}