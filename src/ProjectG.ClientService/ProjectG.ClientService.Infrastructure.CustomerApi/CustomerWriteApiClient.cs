namespace ProjectG.ClientService.Infrastructure.CustomerApi
{
    using System;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Configuration;

    using Newtonsoft.Json;

    using ProjectG.ClientService.Infrastructure.CustomerApi.DTO;
    using ProjectG.ClientService.Infrastructure.CustomerApi.Interfaces;

    public class CustomerWriteApiClient : ICustomerWriteApiClient
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        public CustomerWriteApiClient(
            IHttpClientFactory httpClientFactory, 
            IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public async Task Create(CustomerWriteModel customerModel)
        {
            HttpClient httpClient = this.CreateHttpClient();
            StringContent content = new StringContent(JsonConvert.SerializeObject(customerModel), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync("/api/customer", content);
            response.EnsureSuccessStatusCode();
        }

        private HttpClient CreateHttpClient()
        {
            HttpClient httpClient = this.httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(this.configuration["CustomerApi:BaseWriteApiUrl"]);

            return httpClient;
        }
    }
}