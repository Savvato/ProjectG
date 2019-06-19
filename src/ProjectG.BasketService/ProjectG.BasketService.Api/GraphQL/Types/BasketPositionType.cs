namespace ProjectG.BasketService.Api.GraphQL.Types
{
    using global::GraphQL.Types;

    using ProjectG.BasketService.Core.Models;

    public class BasketPositionType : ObjectGraphType<BasketPosition>
    {
        public BasketPositionType()
        {
            this.Field(position => position.Id, type: typeof(IdGraphType), nullable: false);
            this.Field(position => position.CustomerId, type: typeof(IntGraphType), nullable: false);
            this.Field(position => position.ProductId, type: typeof(IntGraphType), nullable: false);
            this.Field(position => position.ProductName, nullable: false);
            this.Field(position => position.ProductDescription, nullable: false);
            this.Field(position => position.Quantity, nullable: false);
            this.Field(position => position.Price, nullable: false);
        }
    }
}