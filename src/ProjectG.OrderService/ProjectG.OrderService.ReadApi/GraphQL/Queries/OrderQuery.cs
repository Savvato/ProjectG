namespace ProjectG.OrderService.ReadApi.GraphQL.Queries
{
    using global::GraphQL.Types;

    using ProjectG.OrderService.Infrastructure.Interfaces;
    using ProjectG.OrderService.ReadApi.GraphQL.Types;

    public class OrderQuery : ObjectGraphType
    {
        public OrderQuery(IOrderRepository orderRepository)
        {
            this.FieldAsync<OrderType>(
                name: "order",
                arguments: new QueryArguments(
                    new QueryArgument<IdGraphType> { Name = "id" }),
                resolve: async context =>
                {
                    long id = context.GetArgument<long>("id");

                    return await orderRepository.Get(id);
                });

            this.FieldAsync<ListGraphType<OrderType>>(
                name: "orders",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "customerId" }),
                resolve: async context =>
                {
                    long customerId = context.GetArgument<long>("customerId");

                    return await orderRepository.GetByCustomerId(customerId);
                });
        }
    }
}