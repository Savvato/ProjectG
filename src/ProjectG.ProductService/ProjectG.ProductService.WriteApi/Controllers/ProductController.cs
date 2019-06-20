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
        private readonly ICommandHandler<ProductEditModel> editProductCommand;

        public ProductController(
            ICommandHandler<ProductCreationModel> createProductCommand, 
            ICommandHandler<ProductEditModel> editProductCommand)
        {
            this.createProductCommand = createProductCommand;
            this.editProductCommand = editProductCommand;
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

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ProductEditModel productEditModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this.editProductCommand.Execute(productEditModel);

            return this.Accepted();
        }
    }
}