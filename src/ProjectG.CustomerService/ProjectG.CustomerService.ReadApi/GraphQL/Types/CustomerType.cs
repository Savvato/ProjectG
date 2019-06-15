namespace ProjectG.CustomerService.ReadApi.GraphQL.Types
{
    using global::GraphQL.Types;

    using ProjectG.CustomerService.Core.Models;

    public class CustomerType : ObjectGraphType<Customer>
    {
        public CustomerType()
        {
            this.Field(customer => customer.Id, type: typeof(IdGraphType), nullable: false);
            this.Field(customer => customer.FirstName, nullable: false);
            this.Field(customer => customer.Surname, nullable: false);
            this.Field(customer => customer.Address, nullable: false);
            this.Field(customer => customer.Email, nullable: false);
        }
    }
}