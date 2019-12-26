using AKS.Api.Build.Client;
using AKS.App.Core.Data;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AKS.App.Build
{
    public static class DIConfig
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazorise(options =>
            {
                //options.ChangeTextOnKeyPress = true; // optional
            })
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();

            services.AddScoped<IAppState, AppState>();
            services.AddScoped<HeaderApi, HeaderApi>();
            services.AddScoped<CustomerEditApi, CustomerEditApi>();
            services.AddScoped<ProjectEditApi, ProjectEditApi>();
            services.AddScoped<ProjectViewApi, ProjectViewApi>();
            services.AddScoped<TopicViewApi, TopicViewApi>();
            services.AddScoped<TopicEditApi, TopicEditApi>();
            services.AddScoped<CategoryViewApi, CategoryViewApi>();
        }

    }
}
