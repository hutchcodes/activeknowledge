using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AKS.Build.App.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("AKS.Build.App.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("AKS.Build.App.ServerAPI"));

            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
//                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://ActiveKnowledgeAppB2C.onmicrosoft.com/230d2366-a2d3-436f-8646-c9b5708939bb/AKS.Build.API.ReadWrite");
                options.ProviderOptions.DefaultAccessTokenScopes.Add("https://ActiveKnowledgeAppB2C.onmicrosoft.com/40f499c4-fc43-4d1e-9f66-2a5625fe4b67/API.Access");
            });

            AKS.App.Build.DIConfig.ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }
    }
}
