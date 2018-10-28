using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resurgam.AppCore.Interfaces;
using Resurgam.Blazor.App.Services;
using Resurgam.Infrastructure.Data;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.Services;

namespace Resurgam.Blazor.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDevelopmentServices(services);
            //Configure DI
            ConfigureDI(services);

            // Since Blazor is running on the server, we can use an application service
            // to read the forecast data.
            services.AddSingleton<WeatherForecastService>();
        }

        public void Configure(IBlazorApplicationBuilder app)
        {
            app.AddComponent<App>("app");
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
            
        }

        public void ConfigureDevelopmentServices(IServiceCollection services)
        {
            // use in-memory database
            ConfigureTestingServices(services);

            // use real database
            // ConfigureProductionServices(services);

        }

        public void ConfigureTestingServices(IServiceCollection services)
        {
            services.AddDbContext<ResurgamContext>(c =>
            {
                try
                {
                    var ConfigurationManager = services.BuildServiceProvider().GetService<IConfiguration>();
                    c.UseSqlServer(ConfigurationManager.GetConnectionString("ResurgamContext"));

                }
                catch (System.Exception ex)
                {
                    var message = ex.Message;
                }
            });

            var dbContext = services.BuildServiceProvider().GetRequiredService<ResurgamContext>();
            dbContext.Database.Migrate();
            ResurgamContextSeed.SeedAsync(dbContext, null).Wait();
        }
    }
}
