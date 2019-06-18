namespace ProjectG.ClientService.Infrastructure.ProductApi
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

    using ProjectG.ClientService.Infrastructure.ProductApi.DTO;
    using ProjectG.ClientService.Infrastructure.ProductApi.Interfaces;

    public class ProductReadApiClient : IProductReadApiClient
    {
        private readonly GraphQLClient graphQLClient;

        public ProductReadApiClient(IConfiguration configuration)
        {
            this.graphQLClient = new GraphQLClient(new GraphQLClientOptions
            {
                EndPoint = new Uri(configuration["ProductApi:GraphQLEndpoint"])
            });
        }

        public async Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            GraphQLRequest request = new GraphQLRequest
            {
                Query = @"
                    query ProductsQuery {
                        products {
                            id
                            name
                            description
                            price
                            count
                        }
                    }"
            };

            GraphQLResponse response = await this.graphQLClient.PostAsync(request);

            if (response.Errors != null && response.Errors.Any())
            {
                throw new HttpRequestException(response.Errors[0].Message);
            }

            return response.GetDataFieldAs<List<ProductModel>>("products");
        }

        public async Task<ProductModel> GetProductById(long productId)
        {
            GraphQLRequest request = new GraphQLRequest
            {
                Query = @"
                    query ProductQuery($productId: ID!) {
                        product(id: $productId) {
                            id
                            name
                            description
                            price
                            count
                        }
                    }",
                Variables = new { productId = productId }
            };

            GraphQLResponse response = await this.graphQLClient.PostAsync(request);

            if (response.Errors != null && response.Errors.Any())
            {
                throw new HttpRequestException(response.Errors[0].Message);
            }

            return response.GetDataFieldAs<ProductModel>("product");
        }
    }
}