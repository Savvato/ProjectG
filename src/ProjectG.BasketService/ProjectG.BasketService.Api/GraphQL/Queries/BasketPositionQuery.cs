namespace ProjectG.BasketService.Api.GraphQL.Queries
{
    using global::GraphQL.Types;

    using ProjectG.BasketService.Api.GraphQL.Types;
    using ProjectG.BasketService.Infrastructure.Interfaces;

    public class BasketPositionQuery : ObjectGraphType
    {
        public BasketPositionQuery(IBasketRepository basketRepository)
        {
            this.FieldAsync<ListGraphType<BasketPositionType>>(
                name: "basket",
                arguments: new QueryArguments(
                    new QueryArgument<IntGraphType> { Name = "customerId" }),
                resolve: async context =>
                {
                    long customerId = context.GetArgument<long>("customerId");

                    return await basketRepository.GetByCustomerId(customerId);
                });
        }
    }
}