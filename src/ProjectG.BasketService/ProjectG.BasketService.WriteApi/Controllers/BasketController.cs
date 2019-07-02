namespace ProjectG.BasketService.WriteApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.BasketService.WriteApi.DTO;
    using ProjectG.Core;

    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ICommandHandler<BasketPositionCreationModel> createBasketPositionCommand;

        public BasketController(ICommandHandler<BasketPositionCreationModel> createBasketPositionCommand)
        {
            this.createBasketPositionCommand = createBasketPositionCommand;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BasketPositionCreationModel basketPositionCreationModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this.createBasketPositionCommand.Execute(basketPositionCreationModel);
            return this.Accepted();
        }
    }
}