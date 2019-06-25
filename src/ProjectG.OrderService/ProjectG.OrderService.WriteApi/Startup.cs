namespace ProjectG.OrderService.WriteApi
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectG.Core;
    using ProjectG.OrderService.Infrastructure.Db;
    using ProjectG.OrderService.WriteApi.Commands;
    using ProjectG.OrderService.WriteApi.DTO;

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

            services.AddTransient<ICommandHandler<OrderInitModel>, InitializeOrderCreationCommand>();

            services.AddCap(options =>
            {
                options.UsePostgreSql(this.configuration.GetConnectionString("DefaultConnection"));

                options.UseKafka(kafka =>
                {
                    kafka.Servers = this.configuration["Kafka:Servers"];

                    kafka.MainConfig.TryAdd("group.id", "order.write.api");
                    kafka.MainConfig.TryAdd("sasl.username", this.configuration["Kafka:Username"]);
                    kafka.MainConfig.TryAdd("sasl.password", this.configuration["Kafka:Password"]);
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                OrderDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<OrderDbContext>();
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