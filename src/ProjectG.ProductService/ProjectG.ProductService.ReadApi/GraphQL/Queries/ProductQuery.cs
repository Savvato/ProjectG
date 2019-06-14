namespace ProjectG.ProductService.ReadApi.GraphQL.Queries
{
    using global::GraphQL.Types;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Caching.Distributed;

    using ProjectG.ProductService.Core.Interfaces;
    using ProjectG.ProductService.Core.Models;
    using ProjectG.ProductService.ReadApi.Extensions;
    using ProjectG.ProductService.ReadApi.GraphQL.Types;

    public class ProductQuery : ObjectGraphType
    {
        public ProductQuery(IProductRepository productRepository, IDistributedCache cache)
        {
            this.Field<ListGraphType<ProductType>>(
                name: "products",
                resolve: context => productRepository.GetQuery().ToListAsync());

            this.FieldAsync<ProductType>(
                name: "product",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType>() {Name = "id"}),
                resolve: async context =>
                {
                    long id = context.GetArgument<long>("id");

                    string cacheKey = $"product.{id}";
                    Product cachedProduct = await cache.Get<Product>(cacheKey);

                    if (cachedProduct == null)
                    {
                        cachedProduct = await productRepository.GetQuery().FirstOrDefaultAsync(product => product.Id == id);
                        await cache.Set(cacheKey, cachedProduct);
                    }

                    return cachedProduct;
                });
        }
    }
}