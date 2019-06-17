namespace ProjectG.ClientService.Infrastructure.CustomerApi
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

    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;
    using ProjectG.ClientService.Infrastructure.CustomerApi.Interfaces;

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

        public async Task<IEnumerable<CustomerModel>> GetAllCustomers()
        {
            GraphQLRequest request = new GraphQLRequest
            {
                Query = @"
                    query CustomersQuery {
                        customers {
                            id
                            firstName
                            surname
                            address
                            email
                        }
                    }"
            };

            GraphQLResponse response = await this.graphQLClient.PostAsync(request);

            if (response.Errors != null && response.Errors.Any())
            {
                throw new HttpRequestException(response.Errors[0].Message);
            }

            return response.GetDataFieldAs<List<CustomerModel>>("customers");
        }

        public async Task<CustomerModel> GetCustomerById(long customerId)
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
                throw new HttpRequestException(response.Errors[0].Message);
            }

            return response.GetDataFieldAs<CustomerModel>("customer");
        }
    }
}