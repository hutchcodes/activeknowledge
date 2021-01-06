using AKS.Api.Build.Helpers;
using AKS.Common;
using AKS.Infrastructure;
using AKS.Infrastructure.Blobs;
using AKS.Infrastructure.Data;
using AKS.Infrastructure.Interfaces;
using AKS.Infrastructure.Services;
using AutoMapper;
using AutoMapper.EquivalencyExpression;
using Microsoft.AspNetCore.Authentication.AzureADB2C.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading.Tasks;

namespace AKS.App.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConfigSettings.LoadConfigs(configuration, ConfigSettings.ApiType.Build);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureDI(services);

            services.AddAutoMapper((serviceProvider, automapper) =>
            {
                automapper.AddProfile<MapperProfile>();
                automapper.AddCollectionMappers();
                automapper.UseEntityFrameworkCoreModel<AKSContext>(serviceProvider);
            }, typeof(AKSContext).Assembly);


            //services.AddAuthentication(AzureADB2CDefaults.AuthenticationScheme)
                           // .AddAzureADB2C(options => Configuration.Bind("AzureAdB2C", options));

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

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        private void ConfigureDI(IServiceCollection services)
        {
            //services.AddScoped<IClaimsTransformation, UserInfoClaims>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGroupService, GroupService>();


            services.AddSingleton<IConfiguration>(Configuration);
            /*ITelemetryInitializer aiInit = new AppInsightsInitializer(Configuration.GetValue<string>("ApplicationInsights:Tags"));
            services.AddSingleton<ITelemetryInitializer>(aiInit);*/

            services.AddScoped(typeof(IAsyncRepository<>), typeof(EfRepository<>));
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IFileStorageRepository, BlobStorageRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICategoryTopicService, CategoryTopicService>();
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
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();
/*
            app.UseAuthentication();
            app.UseAuthorization();
*/
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
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
                c.UseSqlServer(Configuration.GetConnectionString("AKSContext"))
                //.UseLazyLoadingProxies()
                ;
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
    }
}
