namespace ProjectG.OrderService.ReadApi.GraphQL.Schemas
{
    using global::GraphQL;
    using global::GraphQL.Types;

    using ProjectG.OrderService.ReadApi.GraphQL.Queries;

    public class OrderSchema : Schema
    {
        public OrderSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            this.Query = dependencyResolver.Resolve<OrderQuery>();
        }
    }
}