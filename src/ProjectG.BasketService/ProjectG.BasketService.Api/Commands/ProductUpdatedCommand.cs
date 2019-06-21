namespace ProjectG.BasketService.Api.Commands
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using ProjectG.BasketService.Api.DTO;
    using ProjectG.BasketService.Infrastructure.Interfaces;
    using ProjectG.Core;

    public class ProductUpdatedCommand : ICommandHandler<ProductUpdatedEventModel>
    {
        private readonly IBasketRepository basketRepository;

        public ProductUpdatedCommand(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        public async Task Execute(ProductUpdatedEventModel commandData)
        {
            await this.basketRepository.Get()
                .Where(position => position.ProductId == commandData.Id)
                .ForEachAsync(position =>
                {
                    position.ProductName = commandData.Name;
                    position.ProductDescription = commandData.Description;
                    position.Price = commandData.Price;
                });
            await this.basketRepository.SaveChanges();
        }
    }
}