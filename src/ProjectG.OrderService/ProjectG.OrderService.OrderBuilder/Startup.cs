namespace ProjectG.OrderService.OrderBuilder
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectG.Core;
    using ProjectG.OrderService.Infrastructure;
    using ProjectG.OrderService.Infrastructure.BasketApi;
    using ProjectG.OrderService.Infrastructure.BasketApi.Interfaces;
    using ProjectG.OrderService.Infrastructure.CustomerApi;
    using ProjectG.OrderService.Infrastructure.CustomerApi.Interfaces;
    using ProjectG.OrderService.Infrastructure.Db;
    using ProjectG.OrderService.Infrastructure.Interfaces;
    using ProjectG.OrderService.OrderBuilder.Commands;
    using ProjectG.OrderService.OrderBuilder.DTO;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBasketGraphQLClient, BasketGraphQLClient>();
            services.AddTransient<ICustomerReadApiClient, CustomerReadApiClient>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddTransient<ICommandHandler<OrderInitModel>, CreateOrderCommand>();

            services.AddDbContext<OrderDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString: this.configuration.GetConnectionString("DefaultConnection"),
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(assemblyName: typeof(OrderDbContext).Assembly.GetName().Name);
                        optionsBuilder.EnableRetryOnFailure();
                        optionsBuilder.CommandTimeout(180);
                    });
            });

            services.AddCap(options =>
            {
                options.UsePostgreSql(this.configuration.GetConnectionString("DefaultConnection"));

                options.UseKafka(kafka =>
                {
                    kafka.Servers = this.configuration["Kafka:Servers"];

                    kafka.MainConfig.TryAdd("group.id", "order.builder");
                    kafka.MainConfig.TryAdd("sasl.username", this.configuration["Kafka:Username"]);
                    kafka.MainConfig.TryAdd("sasl.password", this.configuration["Kafka:Password"]);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
        }
    }
}
