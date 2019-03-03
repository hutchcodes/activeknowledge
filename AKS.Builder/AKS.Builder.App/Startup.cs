using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Resurgam.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Resurgam.AppCore.Interfaces;
using Resurgam.Infrastructure.Interfaces;
using Resurgam.Infrastructure.Services;
using Blazor.FileReader;

namespace AKS.Builder.App
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDevelopmentServices(services);
            //Configure DI
            ConfigureDI(services);
        }

        public void Configure(IComponentsApplicationBuilder app)
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

            services.AddScoped<IFileReaderService, FileReaderService>();

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
