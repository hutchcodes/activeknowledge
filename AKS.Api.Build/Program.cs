using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Infrastructure.Data;
using AKS.Infrastructure.Data.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AKS.Api.Build
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                var aksContext = services.GetRequiredService<AKSContext>();
                AKSContextSeed.SeedAsync(aksContext, loggerFactory).Wait();
                var securityContext = services.GetRequiredService<SecurityContext>();
                SecurityContextSeed.SeedAsync(securityContext, loggerFactory).Wait();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
