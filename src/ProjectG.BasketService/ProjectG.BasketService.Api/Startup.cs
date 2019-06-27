namespace ProjectG.BasketService.Api
{
    using global::GraphQL;
    using global::GraphQL.Server;
    using global::GraphQL.Server.Ui.Playground;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectG.BasketService.Api.Commands;
    using ProjectG.BasketService.Api.DTO;
    using ProjectG.BasketService.Api.GraphQL.Queries;
    using ProjectG.BasketService.Api.GraphQL.Schemas;
    using ProjectG.BasketService.Api.GraphQL.Types;
    using ProjectG.BasketService.Infrastructure;
    using ProjectG.BasketService.Infrastructure.Db;
    using ProjectG.BasketService.Infrastructure.Interfaces;
    using ProjectG.BasketService.Infrastructure.ProductApi;
    using ProjectG.BasketService.Infrastructure.ProductApi.Interfaces;
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
                    connectionString: configuration.GetConnectionString("DefaultConnection"),
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(assemblyName: typeof(BasketDbContext).Assembly.GetName().Name);
                        optionsBuilder.EnableRetryOnFailure();
                        optionsBuilder.CommandTimeout(180);
                    });
            }, ServiceLifetime.Singleton);

            services.AddSingleton<IProductReadApiClient, ProductReadApiClient>();

            services.AddSingleton<IBasketRepository, BasketRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();

            services.AddSingleton<ICommandHandler<BasketPositionCreationModel>, CreateBasketPositionCommand>();
            services.AddSingleton<ICommandHandler<ProductUpdatedEventModel>, ProductUpdatedCommand>();
            services.AddSingleton<ICommandHandler<OrderCreatedEventModel>, OrderCreatedCommand>();

            services.AddSingleton<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddSingleton<BasketPositionType>();
            services.AddSingleton<BasketPositionQuery>();
            services.AddSingleton<BasketPositionSchema>();

            services.AddGraphQL(options =>
                {
                    options.ExposeExceptions = this.hostingEnvironment.IsDevelopment();
                })
                .AddGraphTypes(ServiceLifetime.Singleton);

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

            app.UseGraphQL<BasketPositionSchema>(path: "/graphql/basket");

            if (this.hostingEnvironment.IsDevelopment())
            {
                app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions
                {
                    GraphQLEndPoint = "/graphql/basket"
                });
            }
            
            app.UseMvc();
        }
    }
}