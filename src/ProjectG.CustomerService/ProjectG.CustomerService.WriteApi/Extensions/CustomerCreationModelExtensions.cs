namespace ProjectG.CustomerService.WriteApi.Extensions
{
    using ProjectG.CustomerService.Core.Models;
    using ProjectG.CustomerService.WriteApi.DTO;

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