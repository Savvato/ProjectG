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
        private readonly ICommandHandler<OrderCreationModel> createOrderCommand;

        public OrderController(ICommandHandler<OrderCreationModel> createOrderCommand)
        {
            this.createOrderCommand = createOrderCommand;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderCreationModel orderCreationModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this.createOrderCommand.Handle(orderCreationModel);

            return this.Ok();
        }
    }
}