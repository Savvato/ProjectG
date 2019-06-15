﻿namespace ProjectG.CustomerService.WriteApi.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using ProjectG.Core;
    using ProjectG.CustomerService.WriteApi.DTO;

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICommandHandler<CustomerCreationModel> createCustomerCommand;

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