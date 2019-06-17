namespace ProjectG.BasketService.Api.GraphQL.Schemas
{
    using global::GraphQL;
    using global::GraphQL.Types;

    using ProjectG.BasketService.Api.GraphQL.Queries;

    public class BasketPositionSchema : Schema
    {
        public BasketPositionSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            this.Query = dependencyResolver.Resolve<BasketPositionQuery>();
        }
    }
}