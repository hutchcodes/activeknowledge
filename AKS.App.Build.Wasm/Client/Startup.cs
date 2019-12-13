using AKS.Api.Build.Client;
using AKS.App.Core.Data;
using AKS.Common;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace AKS.App.Build.Wasm.Client
{
    public class Startup
    {
        public Startup()
        {
            ConfigSettings.BuildApiBaseUrl = "https://aks-dev-build-api-wa.azurewebsites.net/api/";
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IConfiguration>(Configuration);
            //ITelemetryInitializer aiInit = new AppInsightsInitializer(Configuration.GetValue<string>("ApplicationInsights:Tags"));
            //services.AddSingleton<ITelemetryInitializer>(aiInit);

            var http = new HttpClient();
            http.BaseAddress = new Uri(ConfigSettings.BuildApiBaseUrl);
            
            services.AddSingleton<HttpClient>(http);
            services.AddScoped<IAppState, AppState>();
            services.AddScoped<HeaderApi, HeaderApi>();
            services.AddScoped<CustomerEditApi, CustomerEditApi>();
            services.AddScoped<ProjectEditApi, ProjectEditApi>();
            services.AddScoped<ProjectViewApi, ProjectViewApi>();
            services.AddScoped<TopicViewApi, TopicViewApi>();
            services.AddScoped<TopicEditApi, TopicEditApi>();
            services.AddScoped<CategoryViewApi, CategoryViewApi>();

            //var aiOptions = new ApplicationInsightsServiceOptions
            //{
            //    //DeveloperMode = true
            //};
            //services.AddApplicationInsightsTelemetry();
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
