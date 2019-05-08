using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AKS.Builder.Data;
using AKS.AppCore.Interfaces;
using AKS.Infrastructure.Data;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.Services;
using Blazor.FileReader;
using AKS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AKS.Builder
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();

            ConfigureDI(services);
            ConfigureTestingServices(services);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private void ConfigureDI(IServiceCollection services)
        {
            //services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<AppState>();
            services.AddScoped<IHeaderService, HeaderService>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IFileReaderService, FileReaderService>();

            MapperConfig.ConfigMappers();
        }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            services.AddDbContext<AKSContext>(c =>
            {
                try
                {
                    var ConfigurationManager = services.BuildServiceProvider().GetService<IConfiguration>();
                    c.UseSqlServer(ConfigurationManager.GetConnectionString("AKSContext"));

                }
                catch (System.Exception ex)
                {
                    var message = ex.Message;
                }
            });

            var dbContext = services.BuildServiceProvider().GetRequiredService<AKSContext>();
            dbContext.Database.Migrate();
            AKSContextSeed.SeedAsync(dbContext, null).Wait();
        }
    }
}
