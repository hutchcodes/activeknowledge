using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AKS.App.Build.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
                .AddAzureADB2C(options => Configuration.Bind("AzureAdB2C", options));

            services.Configure<OpenIdConnectOptions>(AzureADB2CDefaults.OpenIdScheme, options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = false

                };
                options.Events = new OpenIdConnectEvents
                {
                    OnTicketReceived = context =>
                    {
                        //Do the on successful stuff
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        //Do the on fail stuff
                        //context.Response.Redirect("/LoginFailed");
                        //context.HandleResponse();
                        return Task.CompletedTask;
                    }
                };
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.None;
            });
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<CSB.Startup>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/DeepLink");
                //endpoints.MapFallbackToClientSideBlazor<CSB.Startup>("index.html");
            });
        }
    }
}
