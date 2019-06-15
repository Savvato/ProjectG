namespace ProjectG.CustomerService.ReadApi.GraphQL.Queries
{
    using global::GraphQL.Types;

    using Microsoft.EntityFrameworkCore;

    using ProjectG.CustomerService.Core.Interfaces;
    using ProjectG.CustomerService.ReadApi.GraphQL.Types;

    public class CustomerQuery : ObjectGraphType
    {
        public CustomerQuery(ICustomerRepository customerRepository)
        {
            this.FieldAsync<ListGraphType<CustomerType>>(
                name: "customers",
                resolve: async context => await customerRepository.Get().ToListAsync());

            this.FieldAsync<CustomerType>(
                name: "customer",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = "Id"}),
                resolve: async context =>
                {
                    long id = context.GetArgument<long>("id");

                    return await customerRepository.Get(id);
                });
        }
    }
}