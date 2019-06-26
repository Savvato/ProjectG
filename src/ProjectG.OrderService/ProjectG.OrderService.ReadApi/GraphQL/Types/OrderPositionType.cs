namespace ProjectG.OrderService.ReadApi.GraphQL.Types
{
    using global::GraphQL.Types;

    using ProjectG.OrderService.Core.Models;

    public class OrderPositionType : ObjectGraphType<OrderPosition>
    {
        public OrderPositionType()
        {
            this.Field(position => position.Id, type: typeof(IdGraphType), nullable: false);
            this.Field(position => position.OrderId, nullable: false);
            this.Field(position => position.ProductId, nullable: false);
            this.Field(position => position.ProductName, nullable: false);
            this.Field(position => position.ProductDescription, nullable: true);
            this.Field(position => position.Count, nullable: false);
            this.Field(position => position.Price, nullable: false);
        }
    }
}