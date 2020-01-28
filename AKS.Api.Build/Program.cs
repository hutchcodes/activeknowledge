using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKS.Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AKS.Api.Build
{
    public static class Program
    {
        static readonly AzureServiceTokenProvider _azureServiceTokenProvider = new AzureServiceTokenProvider();
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                var aksContext = services.GetRequiredService<AKSContext>();
                AKSContextSeed.SeedAsync(aksContext, loggerFactory).Wait();
                //var securityContext = services.GetRequiredService<SecurityContext>();
                //SecurityContextSeed.SeedAsync(securityContext, loggerFactory).Wait();
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var settings = config.Build();

                        var kvClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(_azureServiceTokenProvider.KeyVaultTokenCallback));
                        config.AddAzureAppConfiguration(options =>
                        {
                            //options.Connect(settings["ConnectionStrings:AppConfig-Prod"])
                            options.Connect(settings["ConnectionStrings:AppConfig"])
                                    .UseAzureKeyVault(kvClient);
                        });
                        config.AddJsonFile($"appSettings.{hostingContext.HostingEnvironment.EnvironmentName}.json", 
                            optional: true, reloadOnChange: true);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
