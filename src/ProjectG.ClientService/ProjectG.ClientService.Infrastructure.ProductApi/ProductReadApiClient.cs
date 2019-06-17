namespace ProjectG.ClientService.Infrastructure.ProductApi
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using GraphQL.Client;

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

        public Task<IEnumerable<ProductModel>> GetAllProducts()
        {
            throw new System.NotImplementedException();
        }

        public Task<ProductModel> GetProductById(long productId)
        {
            throw new System.NotImplementedException();
        }
    }
}