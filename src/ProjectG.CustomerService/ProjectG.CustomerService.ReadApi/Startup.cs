namespace ProjectG.CustomerService.ReadApi
{
    using global::GraphQL;
    using global::GraphQL.Server;
    using global::GraphQL.Server.Ui.Playground;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectG.CustomerService.Core.Interfaces;
    using ProjectG.CustomerService.Infrastructure.Db;
    using ProjectG.CustomerService.ReadApi.GraphQL.Queries;
    using ProjectG.CustomerService.ReadApi.GraphQL.Schemas;
    using ProjectG.CustomerService.ReadApi.GraphQL.Types;

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
            services.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString: this.configuration.GetConnectionString("DefaultConnection"),
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(assemblyName: typeof(CustomerDbContext).Assembly.GetName().Name);
                        optionsBuilder.EnableRetryOnFailure();
                        optionsBuilder.CommandTimeout(180);
                    });
            });

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<CustomerType>();
            services.AddScoped<CustomerQuery>();
            services.AddScoped<CustomerSchema>();

            services.AddScoped<IDependencyResolver>(s => new FuncDependencyResolver(s.GetRequiredService));
            services.AddGraphQL(options =>
                {
                    options.ExposeExceptions = this.hostingEnvironment.IsDevelopment();
                })
                .AddGraphTypes(ServiceLifetime.Scoped);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseGraphQL<CustomerSchema>("/customer");
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());
        }
    }
}
