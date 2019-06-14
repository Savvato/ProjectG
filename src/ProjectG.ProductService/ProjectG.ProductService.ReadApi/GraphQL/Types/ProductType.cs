namespace ProjectG.ProductService.ReadApi.GraphQL.Types
{
    using global::GraphQL.Types;

    using ProjectG.ProductService.Core.Models;

    public class ProductType : ObjectGraphType<Product>
    {
        public ProductType()
        {
            this.Field(product => product.Id, type: typeof(IdGraphType), nullable: false);
            this.Field(product => product.Name, nullable: false);
            this.Field(product => product.Description, nullable: false);
            this.Field(product => product.Price, nullable: false);
            this.Field(product => product.Count, nullable: false);
        }
    }
}