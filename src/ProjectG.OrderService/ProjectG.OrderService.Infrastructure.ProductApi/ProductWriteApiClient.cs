﻿namespace ProjectG.OrderService.Infrastructure.ProductApi
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

    using Newtonsoft.Json;

    using ProjectG.OrderService.Infrastructure.ProductApi.DTO;
    using ProjectG.OrderService.Infrastructure.ProductApi.Interfaces;

    public class ProductWriteApiClient : IProductWriteApiClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public ProductWriteApiClient(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task Edit(ProductModel productModel)
        {
            HttpClient httpClient = this.CreateHttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(productModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PutAsync("/api/product", content);
            response.EnsureSuccessStatusCode();
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient httpClient = this.httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(this.configuration["ProductApi:BaseWriteApiUrl"]);

            return httpClient;
        }
    }
}