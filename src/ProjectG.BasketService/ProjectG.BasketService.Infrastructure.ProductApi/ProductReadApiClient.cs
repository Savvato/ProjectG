namespace ProjectG.BasketService.Infrastructure.ProductApi
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    using GraphQL.Client;
    using GraphQL.Common.Request;
    using GraphQL.Common.Response;

    using Microsoft.Extensions.Configuration;

    using ProjectG.BasketService.Infrastructure.ProductApi.Interfaces;
    using ProjectG.BasketService.Infrastructure.ProductApi.Models;

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