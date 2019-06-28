namespace ProjectG.OrderService.OrderBuilder.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DotNetCore.CAP;

    using Microsoft.Extensions.Logging;

    using ProjectG.Core;
    using ProjectG.OrderService.Core.Models;
    using ProjectG.OrderService.Infrastructure.BasketApi.DTO;
    using ProjectG.OrderService.Infrastructure.CustomerApi.DTO;
    using ProjectG.OrderService.Infrastructure.Interfaces;
    using ProjectG.OrderService.OrderBuilder.DTO;

    public class CreateOrderCommand : ICommandHandler<OrderInitModel>, ICapSubscribe
    {
        private const string OrderInitTopicName = "order.init";
        private const string OrderCreatedTopicName = "order.created";

        private readonly IOrderRepository orderRepository;
        private readonly IBasketRepository basketRepository;
        private readonly ICustomerRepository customerRepository;

        private readonly ICapPublisher eventBus;

        private readonly ILogger<CreateOrderCommand> logger;

        public CreateOrderCommand(
            IBasketRepository basketRepository,
            ICustomerRepository customerRepository,
            IOrderRepository orderRepository,
            ICapPublisher eventBus,
            ILogger<CreateOrderCommand> logger)
        {
            this.basketRepository = basketRepository;
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
            this.eventBus = eventBus;
            this.logger = logger;
        }

        [CapSubscribe(OrderInitTopicName)]
        public async Task Execute(OrderInitModel commandData)
        {
            IEnumerable<BasketPositionModel> basket = null;
            CustomerModel customer = null;

            async Task LoadBasketData() => basket = await this.basketRepository.GetCustomerBasket(commandData.CustomerId);
            async Task LoadCustomerData() => customer = await this.customerRepository.Get(commandData.CustomerId);

            await Task.WhenAll(LoadBasketData(), LoadCustomerData());

            Order order = new Order
            {
                CustomerId = commandData.CustomerId,
                DateCreated = DateTime.UtcNow,
                FirstName = customer.FirstName,
                Surname = customer.Surname,
                Status = OrderStatus.Created
            };

            List<OrderPosition> orderPositions = basket.Select(basketPosition => new OrderPosition
                {
                    ProductId = basketPosition.ProductId,
                    ProductName = basketPosition.ProductName,
                    ProductDescription = basketPosition.ProductDescription,
                    Count = basketPosition.Quantity,
                    Price = basketPosition.Price
                })
                .ToList();

            order.OrderPositions = orderPositions;

            await this.orderRepository.Create(order);
            await this.orderRepository.SaveChanges();

            this.logger.LogInformation($"CREATED ORDER ID: {order.Id}");

            await this.eventBus.PublishAsync(
                name: OrderCreatedTopicName,
                contentObj: new OrderCreatedEventModel
                {
                    OrderId = order.Id,
                    CustomerId = order.CustomerId
                });
        }
    }
}