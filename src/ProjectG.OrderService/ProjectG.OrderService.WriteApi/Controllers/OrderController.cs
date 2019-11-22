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
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices] ICommandHandler<OrderInitModel> createOrderCommand,
            [FromBody] OrderInitModel orderInitModel)
        {
            await createOrderCommand.Execute(orderInitModel);

            return this.Ok();
        }

        [HttpPost("status")]
        public async Task<IActionResult> UpdateStatus(
            [FromServices] ICommandHandler<OrderStatusUpdateEventModel> orderStatusUpdateCommand,
            [FromBody] OrderStatusUpdateEventModel orderStatusUpdateEventModel)
        {
            await orderStatusUpdateCommand.Execute(orderStatusUpdateEventModel);

            return this.Ok();
        }
    }
}