namespace ProjectG.ProductService.WriteApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

    using ProjectG.Core;
    using ProjectG.ProductService.Infrastructure;
    using ProjectG.ProductService.Infrastructure.Cache;
    using ProjectG.ProductService.Infrastructure.Cache.Interfaces;
    using ProjectG.ProductService.Infrastructure.Db;
    using ProjectG.ProductService.Infrastructure.Interfaces;
    using ProjectG.ProductService.Infrastructure.OrderApi;
    using ProjectG.ProductService.Infrastructure.OrderApi.Interfaces;
    using ProjectG.ProductService.WriteApi.Commands;
    using ProjectG.ProductService.WriteApi.DTO;

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

            services.AddScoped<IOrderReadApiClient, OrderReadApiClient>();

            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString: this.configuration.GetConnectionString("DefaultConnection"),
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(assemblyName: typeof(ProductDbContext).Assembly.GetName().Name);
                        optionsBuilder.EnableRetryOnFailure();
                        optionsBuilder.CommandTimeout(180);
                    });
            });

            services.AddTransient<ICommandHandler<ProductCreationModel>, CreateProductCommand>();
            services.AddTransient<ICommandHandler<ProductEditModel>, EditProductCommand>();
            services.AddTransient<ICommandHandler<OrderCreatedEventModel>, OrderCreatedCommand>();

            services.AddScoped<IProductCache, ProductCache>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "ProductsCache";
                options.Configuration = this.configuration.GetConnectionString("Redis");
            });

            services.AddCap(options =>
            {
                options.UsePostgreSql(this.configuration.GetConnectionString("CapConnection"));

                options.UseKafka(kafka =>
                {
                    kafka.Servers = this.configuration["Kafka:Servers"];

                    kafka.MainConfig.TryAdd("group.id", "product.api");
                    kafka.MainConfig.TryAdd("sasl.username", this.configuration["Kafka:Username"]);
                    kafka.MainConfig.TryAdd("sasl.password", this.configuration["Kafka:Password"]);
                }); 
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                ProductDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<ProductDbContext>();
                dbContext.Database.Migrate();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
