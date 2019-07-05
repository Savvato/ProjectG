namespace ProjectG.BasketService.WriteApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectG.BasketService.Infrastructure;
    using ProjectG.BasketService.Infrastructure.Cache;
    using ProjectG.BasketService.Infrastructure.Cache.Interfaces;
    using ProjectG.BasketService.Infrastructure.Db;
    using ProjectG.BasketService.Infrastructure.Interfaces;
    using ProjectG.BasketService.Infrastructure.ProductApi;
    using ProjectG.BasketService.Infrastructure.ProductApi.Interfaces;
    using ProjectG.BasketService.WriteApi.Commands;
    using ProjectG.BasketService.WriteApi.DTO;
    using ProjectG.Core;

    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<BasketDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString: this.configuration.GetConnectionString("DefaultConnection"),
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(assemblyName: typeof(BasketDbContext).Assembly.GetName().Name);
                        optionsBuilder.EnableRetryOnFailure();
                        optionsBuilder.CommandTimeout(180);
                    });
            });

            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "BasketCache";
                options.Configuration = this.configuration.GetConnectionString("Redis");
            });

            services.AddScoped<IBasketCache, BasketCache>();

            services.AddScoped<IProductReadApiClient, ProductReadApiClient>();

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddTransient<ICommandHandler<BasketPositionCreationModel>, CreateBasketPositionCommand>();
            services.AddTransient<ICommandHandler<ProductUpdatedEventModel>, ProductUpdatedCommand>();
            services.AddTransient<ICommandHandler<OrderCreatedEventModel>, OrderCreatedCommand>();

            services.AddCap(options =>
            {
                options.UsePostgreSql(this.configuration.GetConnectionString("CapConnection"));

                options.UseKafka(kafka =>
                {
                    kafka.Servers = this.configuration["Kafka:Servers"];

                    kafka.MainConfig.TryAdd("group.id", "basket.api");
                    kafka.MainConfig.TryAdd("sasl.username", this.configuration["Kafka:Username"]);
                    kafka.MainConfig.TryAdd("sasl.password", this.configuration["Kafka:Password"]);
                });
            });
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime applicationLifetime)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                BasketDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<BasketDbContext>();
                dbContext.Database.Migrate();
            }

            if (this.hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            
            
            app.UseMvc();
        }
    }
}