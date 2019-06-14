namespace ProjectG.ProductService.ReadApi.GraphQL.Queries
{
    using System.Linq;

    using global::GraphQL.Types;

    using ProjectG.ProductService.Core.Interfaces;
    using ProjectG.ProductService.ReadApi.GraphQL.Types;

    public class ProductQuery : ObjectGraphType
    {
        public ProductQuery(IProductRepository productRepository)
        {
            this.Field<ListGraphType<ProductType>>(
                name: "products",
                resolve: context => productRepository.GetQuery());
            this.Field<ProductType>(
                name: "product",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType>() {Name = "id"}),
                resolve: context =>
                {
                    long id = context.GetArgument<long>("id");
                    return productRepository.GetQuery().FirstOrDefault(product => product.Id == id);
                });
        }
    }
}