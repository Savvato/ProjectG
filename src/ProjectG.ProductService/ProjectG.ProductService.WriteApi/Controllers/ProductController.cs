namespace ProjectG.ProductService.WriteApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.Core;
    using ProjectG.ProductService.WriteApi.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post(
            ICommandHandler<ProductCreationModel> createProductCommand,
            [FromBody] ProductCreationModel productCreationModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await createProductCommand.Execute(productCreationModel);

            return this.Accepted();
        }

        [HttpPut]
        public async Task<IActionResult> Put(
            [FromServices] ICommandHandler<ProductEditModel> editProductCommand,
            [FromBody] ProductEditModel productEditModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await editProductCommand.Execute(productEditModel);

            return this.Accepted();
        }
    }
}