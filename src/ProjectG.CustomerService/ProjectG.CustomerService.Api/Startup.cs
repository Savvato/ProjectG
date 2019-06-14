namespace ProjectG.CustomerService.Api
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectG.Core;
    using ProjectG.CustomerService.Api.Commands;
    using ProjectG.CustomerService.Api.DTO;
    using ProjectG.CustomerService.Core.Interfaces;
    using ProjectG.CustomerService.Infrastructure.Db;

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

            services.AddDbContext<CustomerDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString: configuration.GetConnectionString("DefaultConnection"),
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(assemblyName: typeof(CustomerDbContext).Assembly.GetName().Name);
                        optionsBuilder.EnableRetryOnFailure();
                        optionsBuilder.CommandTimeout(180);
                    });
            });

            services.AddScoped<ICustomerRepository, CustomerRepository>();

            services.AddScoped<ICommandHandler<CustomerCreationModel>, CreateCustomerCommand>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                CustomerDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<CustomerDbContext>();
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