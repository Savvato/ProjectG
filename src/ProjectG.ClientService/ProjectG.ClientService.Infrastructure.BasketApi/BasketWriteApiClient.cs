namespace ProjectG.ClientService.Infrastructure.BasketApi
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

    using Newtonsoft.Json;

    using ProjectG.ClientService.Infrastructure.BasketApi.DTO;
    using ProjectG.ClientService.Infrastructure.BasketApi.Interfaces;

    public class BasketWriteApiClient : IBasketWriteApiClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public BasketWriteApiClient(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task Create(BasketPositionWriteModel basketPosition)
        {
            HttpClient httpClient = this.CreateHttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(basketPosition), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("/api/basket", content);
            response.EnsureSuccessStatusCode();
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient httpClient = this.httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(this.configuration["BasketApi:BaseWriteApiUrl"]);

            return httpClient;
        }
    }
}