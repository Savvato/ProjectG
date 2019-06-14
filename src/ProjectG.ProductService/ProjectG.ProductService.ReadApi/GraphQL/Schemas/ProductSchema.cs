namespace ProjectG.ProductService.ReadApi.GraphQL.Schemas
{
    using global::GraphQL;
    using global::GraphQL.Types;

    using ProjectG.ProductService.ReadApi.GraphQL.Queries;

    public class ProductSchema : Schema
    {
        public ProductSchema(IDependencyResolver dependencyResolver) : base(dependencyResolver)
        {
            this.Query = dependencyResolver.Resolve<ProductQuery>();
        }
    }
}