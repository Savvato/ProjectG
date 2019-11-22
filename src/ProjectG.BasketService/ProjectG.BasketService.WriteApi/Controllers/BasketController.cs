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
        [HttpPost]
        public async Task<IActionResult> Post(
            [FromServices] ICommandHandler<BasketPositionCreationModel> createBasketPositionCommand,
            [FromBody] BasketPositionCreationModel basketPositionCreationModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await createBasketPositionCommand.Execute(basketPositionCreationModel);
            return this.Accepted();
        }
    }
}