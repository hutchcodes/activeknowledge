using AKS.Api.Build.Client;
using AKS.App.Core.Data;
using AKS.Common;
using Blazored.LocalStorage;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace AKS.App.Build.CSB
{
    public class Startup
    {
        public Startup()
        {
            ConfigSettings.BuildApiBaseUrl = "https://build.activeknowledge.app/api/";
            //ConfigSettings.BuildApiBaseUrl = "https://localhost:44301/api/";
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IConfiguration>(Configuration);
            //ITelemetryInitializer aiInit = new AppInsightsInitializer(Configuration.GetValue<string>("ApplicationInsights:Tags"));
            //services.AddSingleton<ITelemetryInitializer>(aiInit);

            var http = new HttpClient();
            http.BaseAddress = new Uri(ConfigSettings.BuildApiBaseUrl);

            services.AddSingleton<HttpClient>(http);

            services.AddBlazoredLocalStorage();
            
            services.AddAuthorizationCore();
            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationProvider>();
            //services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationProvider>());
            //services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
            //services.AddScoped<AuthenticationStateProvider, MyAuthenticationStateProvider>();


            DIConfig.ConfigureServices(services);

            services.AddAuthorizationCore(options =>
            {
                //options.AddPolicy("read:weather_forecast", policy => policy.RequireClaim("permissions", "read:weather_forecast"));
                //options.AddPolicy("execute:increment_counter", policy => policy.RequireClaim("permissions", "execute:increment_counter"));
            });

            //var aiOptions = new ApplicationInsightsServiceOptions
            //{
            //    //DeveloperMode = true
            //};
            //services.AddApplicationInsightsTelemetry();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

  

            app.AddComponent<AKS.App.Build.App.App>("app");
        }
    }
}
