using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure;
using AKS.Infrastructure.Blobs;
using AKS.Infrastructure.Data;
using AKS.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.ApplicationInsights.Extensibility;
using AKS.Common;
using System;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Threading.Tasks;
using System.Security.Claims;
using AKS.Common.Enums;
using AKS.Api.Build.Helpers;
using Microsoft.AspNetCore.HttpOverrides;
using AutoMapper.EquivalencyExpression;

namespace AKS.Api.Build
{
    public class Startup
    {
        private IServiceCollection? _services;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            ConfigSettings.LoadConfigs(configuration, ConfigSettings.ApiType.Build);
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDI(services);

            services.AddAutoMapper((serviceProvider, automapper) =>
            {
                automapper.AddProfile<MapperProfile>();
                automapper.AddCollectionMappers();
                automapper.UseEntityFrameworkCoreModel<AKSContext>(serviceProvider);
            }, typeof(AKSContext).Assembly);

            services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
                .AddAzureADB2C(options => Configuration.Bind("AzureAdB2C", options));

            var sp = services.BuildServiceProvider();

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
                        var userUpdateHelper = new UserUpdateHelper(sp);
                        return userUpdateHelper.CreateUpdateUser(context.Principal);

                        //return Task.CompletedTask;
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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AKS API", Version = "v1" });
            });

            // Add memory cache services
            services.AddMemoryCache();

            //TODO: CORS is WideOpen
            #region CORS Policy
            services.AddCors(options =>
            {
                options.AddPolicy("WideOpenCors",
                    builder =>
                        builder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin())
                ;
            });
            #endregion


            //services.AddAutoMapper((serviceProvider, automapper) =>
            //{
            //    automapper.AddProfile(typeof(MapperProfile));
            //    automapper.AddCollectionMappers();
            //    automapper.UseEntityFrameworkCoreModel<AKSContext>(serviceProvider);
            //}, typeof(AKSContext).Assembly);

            //services.AddAutoMapper((serviceProvider, automapper) =>
            //{
            //    automapper.AddProfile<MapperProfile>();
            //    automapper.AddCollectionMappers();
            //    automapper.UseEntityFrameworkCoreModel<AKSContext>(serviceProvider);
            //}, typeof(AKSContext).Assembly);
            //services.AddSingleton<IMapper>(MapperConfig.GetMapperConfig().CreateMapper());

            _services = services;
            var aiOptions = new ApplicationInsightsServiceOptions
            {
                //DeveloperMode = true
            };
            services.AddApplicationInsightsTelemetry();

            services.AddRazorPages();
        }

        private void ConfigureDI(IServiceCollection services)
        {
            //services.AddScoped<IClaimsTransformation, UserInfoClaims>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();


            services.AddSingleton<IConfiguration>(Configuration);
            ITelemetryInitializer aiInit = new AppInsightsInitializer(Configuration.GetValue<string>("ApplicationInsights:Tags"));
            services.AddSingleton<ITelemetryInitializer>(aiInit);
            //services.AddSingleton<IMapper>(MapperConfig.GetMapperConfig().CreateMapper());

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
            app.UseCors("WideOpenCors");
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.Use((context, next) =>
            {
                context.Request.Scheme = "https";
                return next();
            });            

            app.UseClientSideBlazorFiles<App.Build.CSB.Startup>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/DeepLink");
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
