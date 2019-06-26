namespace ProjectG.ClientService.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectG.ClientService.Infrastructure;
    using ProjectG.ClientService.Infrastructure.BasketApi;
    using ProjectG.ClientService.Infrastructure.BasketApi.Interfaces;
    using ProjectG.ClientService.Infrastructure.CustomerApi;
    using ProjectG.ClientService.Infrastructure.CustomerApi.Interfaces;
    using ProjectG.ClientService.Infrastructure.Interfaces;
    using ProjectG.ClientService.Infrastructure.OrderApi;
    using ProjectG.ClientService.Infrastructure.OrderApi.Interfaces;
    using ProjectG.ClientService.Infrastructure.ProductApi;
    using ProjectG.ClientService.Infrastructure.ProductApi.Interfaces;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddHttpClient();

            services.AddScoped<ICustomerReadApiClient, CustomerReadApiClient>();
            services.AddScoped<ICustomerWriteApiClient, CustomerWriteApiClient>();
            services.AddScoped<IBasketGraphQLClient, BasketGraphQLClient>();
            services.AddScoped<IBasketWriteApiClient, BasketWriteApiClient>();
            services.AddScoped<IProductReadApiClient, ProductReadApiClient>();
            services.AddScoped<IProductWriteApiClient, ProductWriteApiClient>();
            services.AddScoped<IOrderReadApiClient, OrderReadApiClient>();
            services.AddScoped<IOrderWriteApiClient, OrderWriteApiClient>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}