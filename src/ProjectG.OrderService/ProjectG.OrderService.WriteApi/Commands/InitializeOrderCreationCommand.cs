namespace ProjectG.OrderService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using DotNetCore.CAP;

    using ProjectG.Core;
    using ProjectG.OrderService.WriteApi.DTO;

    public class InitializeOrderCreationCommand : ICommandHandler<OrderInitModel>
    {
        private const string TopicName = "order.init";

        private readonly ICapPublisher eventBus;

        public InitializeOrderCreationCommand(ICapPublisher eventBus)
        {
            this.eventBus = eventBus;
        }

        public async Task Execute(OrderInitModel commandData)
        {
            await this.eventBus.PublishAsync(name: TopicName, contentObj: commandData);
        }
    }
}
