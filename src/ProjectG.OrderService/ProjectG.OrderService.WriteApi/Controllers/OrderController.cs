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

        public OrderController(ICommandHandler<OrderInitModel> createOrderCommand)
        {
            this.createOrderCommand = createOrderCommand;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrderInitModel orderInitModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this.createOrderCommand.Execute(orderInitModel);

            return this.Ok();
        }
    }
}