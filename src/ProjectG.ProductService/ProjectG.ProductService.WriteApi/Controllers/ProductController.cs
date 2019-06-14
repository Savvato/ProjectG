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
        private readonly ICommandHandler<ProductCreationModel> createProductCommand;

        public ProductController(ICommandHandler<ProductCreationModel> createProductCommand)
        {
            this.createProductCommand = createProductCommand;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductCreationModel productCreationModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this.createProductCommand.Execute(productCreationModel);

            return this.Accepted();
        }
    }
}