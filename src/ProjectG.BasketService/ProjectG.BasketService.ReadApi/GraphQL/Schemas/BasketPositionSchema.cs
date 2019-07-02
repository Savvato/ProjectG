namespace ProjectG.BasketService.ReadApi.GraphQL.Schemas
{
    using global::GraphQL;
    using global::GraphQL.Types;

    using ProjectG.BasketService.ReadApi.GraphQL.Queries;

    public class BasketPositionSchema : Schema
    {
        public BasketPositionSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            this.Query = dependencyResolver.Resolve<BasketPositionQuery>();
        }
    }
}