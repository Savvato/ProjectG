namespace ProjectG.BasketService.WriteApi.Commands
{
    using System.Linq;
    using System.Threading.Tasks;

    using DotNetCore.CAP;

    using Microsoft.EntityFrameworkCore;

    using ProjectG.BasketService.Infrastructure.Interfaces;
    using ProjectG.BasketService.WriteApi.DTO;
    using ProjectG.Core;

    public class ProductUpdatedCommand : ICommandHandler<ProductUpdatedEventModel>, ICapSubscribe
    {
        private const string ProductUpdatesTopicName = "product.updates";

        private readonly IBasketRepository basketRepository;

        public ProductUpdatedCommand(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [CapSubscribe(ProductUpdatesTopicName)]
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