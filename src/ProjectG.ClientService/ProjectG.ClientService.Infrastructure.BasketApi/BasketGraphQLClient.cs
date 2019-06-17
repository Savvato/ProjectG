namespace ProjectG.ClientService.Infrastructure.BasketApi
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

    using ProjectG.ClientService.Infrastructure.BasketApi.DTO;
    using ProjectG.ClientService.Infrastructure.BasketApi.Interfaces;

    public class BasketGraphQLClient : IBasketGraphQLClient
    {
        private readonly GraphQLClient graphQLClient;

        public BasketGraphQLClient(IConfiguration configuration)
        {
            this.graphQLClient = new GraphQLClient(new GraphQLClientOptions
            {
                EndPoint = new Uri(configuration["BasketApi:GraphQLEndpoint"])
            });
        }

        public async Task<IEnumerable<BasketPositionModel>> GetCustomerBasket(long customerId)
        {
            GraphQLRequest request = new GraphQLRequest
            {
                Query = @"
                    query BasketQuery($customerId: Int!) {
                        basket(customerId: $customerId) {
                            id
                            customerId
                            productId
                            quantity
                            price
                        }
                    }",
                Variables = new { customerId = customerId }
            };

            GraphQLResponse response = await this.graphQLClient.PostAsync(request);

            if (response.Errors != null && response.Errors.Any())
            {
                throw new HttpRequestException(response.Errors[0].Message);
            }

            return response.GetDataFieldAs<List<BasketPositionModel>>("basket");
        }
    }
}