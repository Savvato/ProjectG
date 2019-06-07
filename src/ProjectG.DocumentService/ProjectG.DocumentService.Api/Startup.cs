namespace ProjectG.DocumentService.Api
{
    #region

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using ProjectG.DocumentService.Infrastructure.Db;

    #endregion

    public class Startup
    {
        private readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<DocumentDbContext>(options =>
            {
                options.UseNpgsql(
                    connectionString: configuration.GetConnectionString("WritingConnection"),
                    optionsBuilder =>
                    {
                        optionsBuilder.MigrationsAssembly(assemblyName: typeof(DocumentDbContext).Assembly.GetName().Name);
                    });
            });

            services.AddDbContextPool<ReadOnlyDocumentDbContext>(options =>
            {
                options.UseNpgsql(connectionString: configuration.GetConnectionString("ReadOnlyConnection"));
            }, poolSize: 128);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}