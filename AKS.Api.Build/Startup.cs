using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure;
using AKS.Infrastructure.Blobs;
using AKS.Infrastructure.Data;
using AKS.Infrastructure.Data.Security;
using AKS.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text;

namespace AKS.Api.Build
{
    public class Startup
    {
        private IServiceCollection? _services;

        public Startup(IConfiguration configuration)
        {
            MapperConfig.ConfigMappers();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDI(services);

            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AKS API", Version = "v1" });
            });

            // Add memory cache services
            services.AddMemoryCache();

            #region CORS Policy
            //TODO: CORS is WideOpen
            services.AddCors(options =>
            {
                options.AddPolicy("WideOpenCors",
                    builder =>
                        builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin());
            });
            #endregion


            _services = services;
        }

        private void ConfigureDI(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);


            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IFileStorageRepository, BlobStorageRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IHeaderService, HeaderService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IProjectService, ProjectService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
                ListAllRegisteredServices(app);

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "AKS API V1");
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
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
            // use in-memory database
            //services.AddDbContext<AKSContext>(c =>
            //    c.UseInMemoryDatabase("AKSDB"));

            services.AddDbContext<AKSContext>(c =>
            {
                // Requires LocalDB which can be installed with SQL Server Express 2016
                // https://www.microsoft.com/en-us/download/details.aspx?id=54284
                c.UseSqlServer(Configuration.GetConnectionString("AKSContext"));
            });

            //services.AddDbContext<SecurityContext>(c =>
            //    c.UseInMemoryDatabase("SecurityDB"));

            ConfigureServices(services);
        }

        public void ConfigureProductionServices(IServiceCollection services)
        {
            // use real database
            services.AddDbContext<AKSContext>(c =>
            {
                // Requires LocalDB which can be installed with SQL Server Express 2016
                // https://www.microsoft.com/en-us/download/details.aspx?id=54284
                c.UseSqlServer(Configuration.GetConnectionString("AKSContext"));
            });
            //services.AddDbContext<SecurityContext>(c =>
            //    c.UseInMemoryDatabase("SecurityDB"));

            ConfigureServices(services);
        }
        private void ListAllRegisteredServices(IApplicationBuilder app)
        {
            if (_services is null) return;

            app.Map("/allservices", builder => builder.Run(async context =>
            {
                var sb = new StringBuilder();
                sb.Append("<h1>All Services</h1>");
                sb.Append("<table><thead>");
                sb.Append("<tr><th>Type</th><th>Lifetime</th><th>Instance</th></tr>");
                sb.Append("</thead><tbody>");
                foreach (var svc in _services)
                {
                    sb.Append("<tr>");
                    sb.Append($"<td>{svc.ServiceType.FullName}</td>");
                    sb.Append($"<td>{svc.Lifetime}</td>");
                    sb.Append($"<td>{svc.ImplementationType?.FullName}</td>");
                    sb.Append("</tr>");
                }
                sb.Append("</tbody></table>");
                await context.Response.Body.WriteAsync(Encoding.ASCII.GetBytes(sb.ToString()));
            }));
        }
    }
}
