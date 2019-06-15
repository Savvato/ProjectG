namespace ProjectG.ProductService.ReadApi.GraphQL.Queries
{
    using global::GraphQL.Types;

    using Microsoft.EntityFrameworkCore;

    using ProjectG.ProductService.Infrastructure.Interfaces;
    using ProjectG.ProductService.ReadApi.GraphQL.Types;

    public class ProductQuery : ObjectGraphType
    {
        public ProductQuery(IProductRepository productRepository)
        {
            this.FieldAsync<ListGraphType<ProductType>>(
                name: "products",
                resolve: async context => await productRepository.Get().ToListAsync());

            this.FieldAsync<ProductType>(
                name: "product",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> {Name = "id"}),
                resolve: async context =>
                {
                    long id = context.GetArgument<long>("id");

                    return await productRepository.Get(id);
                });
        }
    }
}