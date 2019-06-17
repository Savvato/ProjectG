namespace ProjectG.ProductService.ReadApi
{
    using global::GraphQL;
    using global::GraphQL.Server;
    using global::GraphQL.Server.Ui.Playground;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectG.ProductService.Infrastructure;
    using ProjectG.ProductService.Infrastructure.Cache;
    using ProjectG.ProductService.Infrastructure.Cache.Interfaces;
    using ProjectG.ProductService.Infrastructure.Db;
    using ProjectG.ProductService.Infrastructure.Interfaces;
    using ProjectG.ProductService.ReadApi.GraphQL.Queries;
    using ProjectG.ProductService.ReadApi.GraphQL.Schemas;
    using ProjectG.ProductService.ReadApi.GraphQL.Types;

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

            services.AddScoped<IProductCache, ProductCache>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));

            services.AddScoped<ProductType>();
            services.AddScoped<ProductQuery>();
            services.AddScoped<ProductSchema>();

            services.AddGraphQL(options =>
                {
                    options.ExposeExceptions = this.hostingEnvironment.IsDevelopment();
                })
                .AddGraphTypes(ServiceLifetime.Scoped);

            services.AddDistributedRedisCache(options =>
            {
                options.InstanceName = "ProductsCache";
                options.Configuration = this.configuration.GetConnectionString("Redis");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (this.hostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL<ProductSchema>(path: "/graphql/product");

            if (this.hostingEnvironment.IsDevelopment())
            {
                app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions
                {
                    GraphQLEndPoint = "/graphql/product"
                });
            }
        }
    }
}