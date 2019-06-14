namespace ProjectG.CustomerService.Api.Extensions
{
    using ProjectG.CustomerService.Api.DTO;
    using ProjectG.CustomerService.Core.Models;

    public static class CustomerCreationModelExtensions
    {
        public static Customer ToCustomer(this CustomerCreationModel customerCreationModel)
        {
            return new Customer
            {
                FirstName = customerCreationModel.FirstName,
                Surname = customerCreationModel.Surname,
                Address = customerCreationModel.Address,
                Email = customerCreationModel.Email
            };
        }
    }
}