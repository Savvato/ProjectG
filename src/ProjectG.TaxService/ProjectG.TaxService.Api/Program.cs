﻿namespace ProjectG.TaxService.Api
{
    #region

    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    #endregion

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}