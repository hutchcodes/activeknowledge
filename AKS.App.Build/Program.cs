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
        static readonly AzureServiceTokenProvider _azureServiceTokenProvider = new AzureServiceTokenProvider();
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

                        var kvClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(_azureServiceTokenProvider.KeyVaultTokenCallback));
#if !DEBUG
                        config.AddAzureAppConfiguration(options => {
                            options.Connect(settings["ConnectionStrings:AppConfig"])
                                    .UseAzureKeyVault(kvClient);
                        });
#endif
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
