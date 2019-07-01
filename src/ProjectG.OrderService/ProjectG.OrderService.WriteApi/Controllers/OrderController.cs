namespace ProjectG.OrderService.WriteApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.Core;
    using ProjectG.OrderService.WriteApi.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ICommandHandler<OrderInitModel> createOrderCommand;
        private readonly ICommandHandler<OrderStatusUpdateEventModel> orderStatusUpdateCommand;

        public OrderController(
            ICommandHandler<OrderInitModel> createOrderCommand, 
            ICommandHandler<OrderStatusUpdateEventModel> orderStatusUpdateCommand)
        {
            this.createOrderCommand = createOrderCommand;
            this.orderStatusUpdateCommand = orderStatusUpdateCommand;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderInitModel orderInitModel)
        {
            await this.createOrderCommand.Execute(orderInitModel);

            return this.Ok();
        }

        [HttpPost("status")]
        public async Task<IActionResult> UpdateStatus([FromBody] OrderStatusUpdateEventModel orderStatusUpdateEventModel)
        {
            await this.orderStatusUpdateCommand.Execute(orderStatusUpdateEventModel);

            return this.Ok();
        }
    }
}