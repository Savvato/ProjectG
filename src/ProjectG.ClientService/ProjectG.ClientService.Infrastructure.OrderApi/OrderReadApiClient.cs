namespace ProjectG.ClientService.Infrastructure.OrderApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using GraphQL.Client;
    using GraphQL.Common.Request;
    using GraphQL.Common.Response;

    using Microsoft.Extensions.Configuration;

    using ProjectG.ClientService.Infrastructure.OrderApi.DTO;
    using ProjectG.ClientService.Infrastructure.OrderApi.Interfaces;

    public class OrderReadApiClient : IOrderReadApiClient
    {
        private readonly GraphQLClientOptions options;

        public OrderReadApiClient(IConfiguration configuration)
        {
            this.options = new GraphQLClientOptions
            {
                EndPoint = new Uri(configuration["OrderApi:GraphQLEndpoint"])
            };
        }

        public async Task<OrderModel> GetById(long orderId)
        {
            GraphQLRequest request = new GraphQLRequest
            {
                Query = @"
                    query OrderQuery($productId: ID!) {
                        order(id: $orderId) {
                            id
                            customerId
                            firstName
                            surname
                            dateCreated
                            status
                            orderPositions {
                                Id
                                OrderId
                                ProductId
                                ProductName
                                ProductDescription
                                Count
                                Price
                            }
                        }
                    }",
                Variables = new { orderId = orderId }
            };

            using (GraphQLClient graphQLClient = new GraphQLClient(this.options))
            {
                GraphQLResponse response = await graphQLClient.PostAsync(request);

                if (response.Errors != null && response.Errors.Any())
                {
                    throw new HttpRequestException(response.Errors[0].Message);
                }

                return response.GetDataFieldAs<OrderModel>("order");
            }
        }

        public Task<IEnumerable<OrderModel>> GetByCustomerId(long customerId)
        {
            throw new System.NotImplementedException();
        }
    }
}