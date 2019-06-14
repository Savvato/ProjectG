namespace ProjectG.CustomerService.Api.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.Core;
    using ProjectG.CustomerService.Api.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICommandHandler<CustomerCreationModel> createCustomerCommand;

        public CustomerController(ICommandHandler<CustomerCreationModel> createCustomerCommand)
        {
            this.createCustomerCommand = createCustomerCommand;
        }

        public async Task<IActionResult> Post([FromBody] CustomerCreationModel customerCreationModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(this.ModelState);
            }

            await this.createCustomerCommand.Execute(customerCreationModel);

            return this.Accepted();
        }
    }
}