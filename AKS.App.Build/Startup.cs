using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AKS.App.Build.Areas.Identity;
using AKS.App.Build.Data;
using AKS.App.Core.Data;
using AKS.Api.Build.Client;
using Microsoft.ApplicationInsights.Extensibility;
using AKS.Common;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;

namespace AKS.App.Build
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);
            ITelemetryInitializer aiInit = new AppInsightsInitializer(Configuration.GetValue<string>("ApplicationInsights:Tags"));
            services.AddSingleton<ITelemetryInitializer>(aiInit);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            services.AddScoped<IAppState, AppState>();
            services.AddScoped<HeaderApi, HeaderApi>();
            services.AddScoped<CustomerEditApi, CustomerEditApi>();
            services.AddScoped<ProjectEditApi, ProjectEditApi>();
            services.AddScoped<ProjectViewApi, ProjectViewApi>();
            services.AddScoped<TopicViewApi, TopicViewApi>();
            services.AddScoped<TopicEditApi, TopicEditApi>();
            services.AddScoped<CategoryViewApi, CategoryViewApi>();

            var aiOptions = new ApplicationInsightsServiceOptions
            {
                //DeveloperMode = true
            };
            services.AddApplicationInsightsTelemetry();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
#pragma warning disable CA1822 // Mark members as static, we may want to access member variables later.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#pragma warning restore CA1822 // Mark members as static
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
