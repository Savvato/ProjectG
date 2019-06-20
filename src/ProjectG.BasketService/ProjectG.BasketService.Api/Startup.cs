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

    using ProjectG.BasketService.Api.Background;
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
            });

            services.AddScoped<IProductReadApiClient, ProductReadApiClient>();

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<ICommandHandler<BasketPositionCreationModel>, CreateBasketPositionCommand>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddScoped<BasketPositionType>();
            services.AddScoped<BasketPositionQuery>();
            services.AddScoped<BasketPositionSchema>();

            services.AddGraphQL(options =>
                {
                    options.ExposeExceptions = this.hostingEnvironment.IsDevelopment();
                })
                .AddGraphTypes(ServiceLifetime.Scoped);

            services.AddHostedService<ProductUpdatesListener>();
            services.AddTransient<ProductUpdatesListener>();
        }

        public void Configure(IApplicationBuilder app, IApplicationLifetime applicationLifetime)
        {
            applicationLifetime.ApplicationStarted.Register(() =>
            {
                ProductUpdatesListener productUpdatesListener = app.ApplicationServices.GetRequiredService<ProductUpdatesListener>();
                productUpdatesListener.StartAsync(applicationLifetime.ApplicationStopping);
            });
            applicationLifetime.ApplicationStopping.Register(async () =>
            {
                ProductUpdatesListener productUpdatesListener = app.ApplicationServices.GetRequiredService<ProductUpdatesListener>();
                await productUpdatesListener.StopAsync(applicationLifetime.ApplicationStopping);
            });

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