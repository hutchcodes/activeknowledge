using AKS.Infrastructure.Data;
using Azure.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.App.Server
{
    public class Program
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
                        var connection = settings.GetConnectionString("AppConfig");

                        var credentials = GetDefaultAzureCredential();
                        config.AddAzureAppConfiguration(options =>
                        {
                            options.Connect(connection)
                                .ConfigureKeyVault(kv =>
                                {
                                    kv.SetCredential(credentials);
                                });
                        });

                        config.AddJsonFile($"appSettings.{hostingContext.HostingEnvironment.EnvironmentName}.json",
                            optional: true, reloadOnChange: true);
                    });

                    webBuilder.UseStartup<Startup>();
                });
        private static DefaultAzureCredential GetDefaultAzureCredential() => new DefaultAzureCredential(new DefaultAzureCredentialOptions
        {
            //be explicit about this to prevent frustration
            ExcludeManagedIdentityCredential = false,
            ExcludeAzureCliCredential = false,
            ExcludeSharedTokenCacheCredential = true,
            ExcludeVisualStudioCodeCredential = true,
            ExcludeInteractiveBrowserCredential = true,
            ExcludeEnvironmentCredential = true,
            ExcludeVisualStudioCredential = true
        });
    }
}
