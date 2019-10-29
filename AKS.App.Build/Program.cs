using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

namespace AKS.App.Build
{
    public static class Program
    {
        static AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseEnvironment(Environments.Development)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var settings = config.Build();
#if !DEBUG
                        var kvClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                        config.AddAzureAppConfiguration(options => {
                            options.Connect(settings["ConnectionStrings:AppConfig"])
                                    .UseAzureKeyVault(kvClient);
                        });
                        config.AddAzureAppConfiguration(settings["ConnectionStrings:AppConfig"]);
#endif
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
