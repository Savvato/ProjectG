namespace ProjectG.BasketService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using DotNetCore.CAP;

    using ProjectG.BasketService.Infrastructure.Interfaces;
    using ProjectG.BasketService.WriteApi.DTO;
    using ProjectG.Core;

    public class OrderCreatedCommand : ICommandHandler<OrderCreatedEventModel>, ICapSubscribe
    {
        private const string OrderCreatedTopicName = "order.created";
        private const string BasketIsClearedTopicName = "basket.cleared";

        private readonly IBasketRepository basketRepository;
        private readonly ICapPublisher eventBus;

        public OrderCreatedCommand(IBasketRepository basketRepository, ICapPublisher eventBus)
        {
            this.basketRepository = basketRepository;
            this.eventBus = eventBus;
        }

        [CapSubscribe(OrderCreatedTopicName)]
        public async Task Execute(OrderCreatedEventModel commandData)
        {
            await this.basketRepository.RemoveCustomerBasket(commandData.CustomerId);

            await this.eventBus.PublishAsync(
                name: BasketIsClearedTopicName,
                contentObj: new CustomerBasketIsClearedEventModel
                {
                    CustomerId = commandData.CustomerId,
                    OrderId = commandData.OrderId
                });
        }

        public class CustomerBasketIsClearedEventModel
        {
            public long CustomerId { get; set; }

            public long OrderId { get; set; }
        }
    }
}