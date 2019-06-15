namespace ProjectG.CustomerService.ReadApi.GraphQL.Schemas
{
    using global::GraphQL;
    using global::GraphQL.Types;

    using ProjectG.CustomerService.ReadApi.GraphQL.Queries;

    public class CustomerSchema : Schema
    {
        public CustomerSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            this.Query = dependencyResolver.Resolve<CustomerQuery>();
        }
    }
}