namespace ProjectG.CustomerService.WriteApi.Commands
{
    using System.Threading.Tasks;

    using ProjectG.Core;
    using ProjectG.CustomerService.Core.Interfaces;
    using ProjectG.CustomerService.Core.Models;
    using ProjectG.CustomerService.WriteApi.DTO;
    using ProjectG.CustomerService.WriteApi.Extensions;

    public class CreateCustomerCommand : ICommandHandler<CustomerCreationModel>
    {
        private readonly ICustomerRepository customerRepository;

        public CreateCustomerCommand(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task Execute(CustomerCreationModel commandData)
        {
            Customer customer = commandData.ToCustomer();
            await this.customerRepository.Add(customer);
        }
    }
}
