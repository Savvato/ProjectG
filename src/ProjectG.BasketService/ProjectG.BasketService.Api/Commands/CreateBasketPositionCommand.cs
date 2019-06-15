namespace ProjectG.BasketService.Api.Commands
{
    using System.Threading.Tasks;

    using ProjectG.BasketService.Api.DTO;
    using ProjectG.BasketService.Api.Extensions;
    using ProjectG.BasketService.Core.Models;
    using ProjectG.BasketService.Infrastructure.Interfaces;
    using ProjectG.Core;

    public class CreateBasketPositionCommand : ICommandHandler<BasketPositionCreationModel>
    {
        private readonly IBasketRepository basketRepository;

        public CreateBasketPositionCommand(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        public async Task Execute(BasketPositionCreationModel commandData)
        {
            BasketPosition basketPosition = commandData.ToBasketPosition();

            await this.basketRepository.Add(basketPosition);
        }
    }
}