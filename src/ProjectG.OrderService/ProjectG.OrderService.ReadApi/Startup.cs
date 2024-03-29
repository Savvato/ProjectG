﻿namespace ProjectG.OrderService.ReadApi
{
    using global::GraphQL;
    using global::GraphQL.Server;
    using global::GraphQL.Server.Ui.Playground;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectG.OrderService.Infrastructure;
    using ProjectG.OrderService.Infrastructure.Db;
    using ProjectG.OrderService.Infrastructure.Interfaces;
    using ProjectG.OrderService.ReadApi.GraphQL.Queries;
    using ProjectG.OrderService.ReadApi.GraphQL.Schemas;
    using ProjectG.OrderService.ReadApi.GraphQL.Types;

    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IHostingEnvironment hostingEnvironment;

        public Startup(
            IConfiguration configuration, 
            IHostingEnvironment hostingEnvironment)
        {
            this.configuration = configuration;
            this.hostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<OrderType>();
            services.AddScoped<OrderStatusType>();
            services.AddScoped<OrderPositionType>();
            services.AddScoped<OrderQuery>();
            services.AddScoped<OrderSchema>();

            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddMvc();

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

            services.AddGraphQL(options =>
                {
                    options.ExposeExceptions = this.hostingEnvironment.IsDevelopment();
                })
                .AddGraphTypes(ServiceLifetime.Scoped);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                using (OrderDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<OrderDbContext>())
                {
                    dbContext.Database.Migrate();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL<OrderSchema>(path: "/graphql/order");

            if (this.hostingEnvironment.IsDevelopment())
            {
                app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions
                {
                    GraphQLEndPoint = "/graphql/order"
                });
            }
        }
    }
}
