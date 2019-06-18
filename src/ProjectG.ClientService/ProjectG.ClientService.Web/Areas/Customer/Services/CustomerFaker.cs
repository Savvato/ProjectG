namespace ProjectG.ClientService.Web.Areas.Customer.Services
{
    using Bogus;

    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;

    public sealed class CustomerFaker : Faker<CustomerWriteModel>
    {
        public CustomerFaker()
        {
            this.RuleFor(customer => customer.FirstName, faker => faker.Person.FirstName);
            this.RuleFor(customer => customer.Surname, faker => faker.Person.LastName);
            this.RuleFor(customer => customer.Address, faker => faker.Person.Address.Street);
            this.RuleFor(customer => customer.Email, faker => faker.Person.Email);
        }
    }
}