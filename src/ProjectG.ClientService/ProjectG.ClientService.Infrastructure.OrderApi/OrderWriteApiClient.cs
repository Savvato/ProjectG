namespace ProjectG.ClientService.Infrastructure.OrderApi
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

    using Newtonsoft.Json;

    using ProjectG.ClientService.Infrastructure.OrderApi.DTO;
    using ProjectG.ClientService.Infrastructure.OrderApi.Interfaces;

    public class OrderWriteApiClient : IOrderWriteApiClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public OrderWriteApiClient(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task Create(OrderCreationRequestModel requestModel)
        {
            HttpClient httpClient = this.CreateHttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(requestModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("/api/order", content);
            response.EnsureSuccessStatusCode();
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient httpClient = this.httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(this.configuration["OrderApi:BaseWriteApiUrl"]);

            return httpClient;
        }
    }
}