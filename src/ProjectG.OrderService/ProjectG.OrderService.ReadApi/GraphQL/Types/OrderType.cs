namespace ProjectG.OrderService.ReadApi.GraphQL.Types
{
    using global::GraphQL.Types;

    using ProjectG.OrderService.Core.Models;

    public class OrderType : ObjectGraphType<Order>
    {
        public OrderType()
        {
            this.Field(order => order.Id, type: typeof(IdGraphType), nullable: false);
            this.Field(order => order.CustomerId, nullable: false);
            this.Field(order => order.FirstName, nullable: false);
            this.Field(order => order.Surname, nullable: false);
            this.Field(order => order.DateCreated, nullable: false);
            this.Field<OrderStatusType>(nameof(Order.Status));
        }
    }
}