namespace ProjectG.BasketService.Api.Commands
{
    using System.Threading.Tasks;

    using DotNetCore.CAP;

    using ProjectG.BasketService.Api.DTO;
    using ProjectG.BasketService.Infrastructure.Interfaces;
    using ProjectG.Core;

    public class OrderCreatedCommand : ICommandHandler<OrderCreatedEventModel>, ICapSubscribe
    {
        private const string OrderCreatedTopicName = "order.created";

        private readonly IBasketRepository basketRepository;

        public OrderCreatedCommand(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

        [CapSubscribe(OrderCreatedTopicName)]
        public async Task Execute(OrderCreatedEventModel commandData)
        {
            await this.basketRepository.RemoveCustomerBasket(commandData.CustomerId);
        }
    }
}