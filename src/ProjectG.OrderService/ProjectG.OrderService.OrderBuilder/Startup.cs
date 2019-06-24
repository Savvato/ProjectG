namespace ProjectG.OrderService.OrderBuilder
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectG.OrderService.OrderBuilder.Background;

    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<QueueConsumer>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        }
    }
}
