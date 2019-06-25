namespace ProjectG.OrderService.Infrastructure.CustomerApi
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using GraphQL.Client;
    using GraphQL.Common.Request;
    using GraphQL.Common.Response;

    using Microsoft.Extensions.Configuration;

    using ProjectG.OrderService.Infrastructure.CustomerApi.DTO;
    using ProjectG.OrderService.Infrastructure.CustomerApi.Interfaces;

    public class CustomerReadApiClient : ICustomerReadApiClient
    {
        private readonly GraphQLClient graphQLClient;

        public CustomerReadApiClient(IConfiguration configuration)
        {
            this.graphQLClient = new GraphQLClient(new GraphQLClientOptions
            {
                EndPoint = new Uri(configuration["CustomerApi:GraphQLEndpoint"])
            });
        }

        public async Task<CustomerModel> Get(long customerId)
        {
            GraphQLRequest request = new GraphQLRequest
            {
                Query = @"
                    query CustomerQuery($customerId: ID!) {
                        customer(id: $customerId) {
                            id
                            firstName
                            surname
                            address
                            email
                        }
                    }",
                Variables = new { customerId = customerId }
            };

            GraphQLResponse response = await this.graphQLClient.PostAsync(request);

            if (response.Errors != null && response.Errors.Any())
            {
                throw new HttpRequestException(response.Errors[0]?.Message);
            }

            return response.GetDataFieldAs<CustomerModel>("customer");
        }
    }
}