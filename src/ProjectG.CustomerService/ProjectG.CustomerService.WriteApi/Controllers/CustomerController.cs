namespace ProjectG.CustomerService.WriteApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.Core;
    using ProjectG.CustomerService.WriteApi.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        public async Task<IActionResult> Post(
            [FromServices] ICommandHandler<CustomerCreationModel> createCustomerCommand,
            [FromBody] CustomerCreationModel customerCreationModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await createCustomerCommand.Execute(customerCreationModel);

            return this.Accepted();
        }
    }
}