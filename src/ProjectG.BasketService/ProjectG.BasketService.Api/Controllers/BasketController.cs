namespace ProjectG.BasketService.Api.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.BasketService.Api.DTO;
    using ProjectG.Core;

    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly ICommandHandler<BasketPositionCreationModel> createBasketPositonCommand;

        public BasketController(ICommandHandler<BasketPositionCreationModel> createBasketPositonCommand)
        {
            this.createBasketPositonCommand = createBasketPositonCommand;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BasketPositionCreationModel basketPositionCreationModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this.createBasketPositonCommand.Execute(basketPositionCreationModel);
            return this.Accepted();
        }
    }
}