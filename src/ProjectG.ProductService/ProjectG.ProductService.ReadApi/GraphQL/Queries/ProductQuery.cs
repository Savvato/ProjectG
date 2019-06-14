namespace ProjectG.ProductService.ReadApi.GraphQL.Queries
{
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
        }
    }
}