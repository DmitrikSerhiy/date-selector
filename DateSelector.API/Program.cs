using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace DateSelector.API {
    public class Program {
        public static void Main(String[] args) {
            var webHost = CreateHostBuilder(args).Build();
            MigrationHelper.SeedAndMigrate(webHost);
            webHost.Run();
        }

        public static IWebHostBuilder CreateHostBuilder(String[] args) =>
            WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
