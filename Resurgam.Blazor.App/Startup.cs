using Microsoft.AspNetCore.Blazor.Builder;
using Microsoft.EntityFrameworkCore;
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
                    // Requires LocalDB which can be installed with SQL Server Express 2016
                    // https://www.microsoft.com/en-us/download/details.aspx?id=54284
                    //c.UseSqlServer(Configuration.GetConnectionString("Server=tcp:rwadevtest.database.windows.net,1433;Initial Catalog=RWADev;Persist Security Info=False;User ID=rwaadmin;Password=Passnow1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"));
#if DEBUG
                    c.UseSqlServer("Server=6LJTLH2\\SQLExpress;Initial Catalog=Resurgam;Persist Security Info=False;Integrated Security=SSPI;");
#else
                    c.UseSqlServer("Server=6LJTLH2\\SQLExpress;Initial Catalog=RWA;Persist Security Info=False;Integrated Security=SSPI;");
#endif
                }
                catch (System.Exception ex)
                {
                    var message = ex.Message;
                }
            });
            //services.AddEntityFrameworkInMemoryDatabase().AddDbContextPool<RWAContext>(options =>
            //{
            //    options.UseInMemoryDatabase("RWAdb");
            //    options.UseLoggerFactory(new LoggerFactory(new[]
            //    {
            //        new ConsoleLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information, 
            //        true)
            //    }));
            //    options.EnableSensitiveDataLogging(true);
            //});
            // use in-memory database
            //services.AddDbContextPool<RWAContext>(c =>
            //    c.UseInMemoryDatabase("RWAdb"));
            var dbContext = services.BuildServiceProvider().GetRequiredService<ResurgamContext>();
            //dbContext = services.BuildServiceProvider().CreateScope().ServiceProvider.GetRequiredService<RWAContext>();
            ResurgamContextSeed.SeedAsync(dbContext, null).Wait();
            //services.AddDbContext<SecurityContext>(c =>
            //    c.UseInMemoryDatabase("SecurityDB"));

            //ConfigureServices(services);
        }
    }
}
