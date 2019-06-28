namespace ProjectG.ProductService.Infrastructure.OrderApi
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using GraphQL.Client;
    using GraphQL.Common.Request;
    using GraphQL.Common.Response;

    using Microsoft.Extensions.Configuration;

    using ProjectG.ProductService.Infrastructure.OrderApi.DTO;
    using ProjectG.ProductService.Infrastructure.OrderApi.Interfaces;

    public class OrderReadApiClient : IOrderReadApiClient
    {
        private readonly GraphQLClientOptions graphQLClientOptions;

        public OrderReadApiClient(IConfiguration configuration)
        {
            this.graphQLClientOptions = new GraphQLClientOptions
            {
                EndPoint = new Uri(configuration["OrderApi:GraphQLEndpoint"])
            };
        }

        public async Task<OrderModel> GetOrderById(long orderId)
        {
            GraphQLRequest request = new GraphQLRequest
            {
                Query = @"
                    query OrderQuery($orderId: ID!) {
                        order(id: $orderId) {
                            id
                            customerId
                            firstName
                            surname
                            dateCreated
                            status
                            orderPositions {
                                id
                                orderId
                                productId
                                productName
                                productDescription
                                count
                                price
                            }
                        }
                    }",
                Variables = new { orderId = orderId }
            };

            using (GraphQLClient graphQLClient = new GraphQLClient(this.graphQLClientOptions))
            {
                GraphQLResponse response = await graphQLClient.PostAsync(request);

                if (response.Errors != null && response.Errors.Any())
                {
                    throw new HttpRequestException(response.Errors[0].Message);
                }

                return response.GetDataFieldAs<OrderModel>("order");
            }
        }
    }
}