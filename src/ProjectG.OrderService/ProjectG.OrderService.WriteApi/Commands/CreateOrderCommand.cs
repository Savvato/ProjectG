namespace ProjectG.OrderService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using ProjectG.Core;
    using ProjectG.OrderService.WriteApi.DTO;

    public class CreateOrderCommand : ICommandHandler<OrderCreationModel>
    {
        public async Task Handle(OrderCreationModel commandData)
        {
            throw new System.NotImplementedException();
        }
    }
}
