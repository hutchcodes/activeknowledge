using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Resurgam.Infrastructure.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Resurgam.AppCore.Entities;

namespace Resurgam.Web.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var resurgamContext = services.GetRequiredService<ResurgamContext>();
                    ResurgamContextSeed.SeedAsync(resurgamContext, loggerFactory).Wait();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex, "An error occured seeding the DB.");
                }
            }

            var resurgamContext2 = host.Services.CreateScope().ServiceProvider.GetRequiredService<ResurgamContext>();
            //var top = from t in resurgamContext2.Topics
            //    .Include($"{nameof(Topic.ReferencedTopics)}.{nameof(RelatedTopic.ReferencedTopic)}")
            //    .Include($"{nameof(Topic.CollectionElements)}.{nameof(CollectionElement.ElementTopics)}")
            //          where t.Id == 555
            //          select t;

            var topSpec = new AppCore.Specifications.TopicDisplaySpecification(1234, 555);
            var includes = topSpec.IncludeStrings;
            var top2 = resurgamContext2.Topics
                .Include(topSpec.Includes[1])
                //.Include(topSpec.Includes[1])
                //.Include(topSpec.Includes[2])
                .FirstOrDefault(topSpec.CompiledCriteria);

            top2 = null;

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
