using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Deploy.DAL;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace Deploy
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddJsonFile("azurekeyvault.json", optional: false, reloadOnChange: true);
                

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets<Startup>();
            }
            builder.AddEnvironmentVariables();

            var config = builder.Build();

            builder.AddAzureKeyVault(
                $"https://{config["azureKeyVault:vault"]}.vault.azure.net/",
                config["azureKeyVault:clientId"],
                config["azureKeyVault:clientSecret"]

                );
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            //Logging
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddDebug();
            });

            //services.AddDbContext<DeployDBContext>(options => options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));
            services.AddDbContext<DeployDBContext>(options => options.UseSqlServer(Configuration["appSettings:connectionStrings:Deploy"]));
            services.AddOptions();
            services.Configure<AzureStorageConfig>(Configuration.GetSection("AzureStorageConfig"));

            //Auth
            services.AddAuthentication(
            SharedOptions =>
            {
                SharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(SharedOptions =>
            {
                SharedOptions.Authority = Configuration["Authentication:AzureAd:AADInstance"] + Configuration["Authentication:AzureAd:TenantId"];
                SharedOptions.ClientSecret = Configuration["Authentication:AzureAd:ClientSecret"];
                SharedOptions.ClientId = Configuration["Authentication:AzureAd:ClientId"];
                SharedOptions.CallbackPath = Configuration["Authentication:AzureAd:CallbackPath"];
                SharedOptions.ResponseType = OpenIdConnectResponseType.IdToken;
            });

            services.AddAuthorization(options =>
               {
                   options.AddPolicy("Admins", policyBuilder => policyBuilder.RequireRole("DeployAdmins"));
               });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/SignIn";
                    options.LogoutPath = "/Account/SignOut";
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            //app.UseAuthentication(new OpenIdConnectOptions
            //{
            //    ClientId = Configuration["Authentication:AzureAd:ClientId"],
            //    ClientSecret = Configuration["Authentication:AzureAd:ClientSecret"],
            //    Authority = Configuration["Authentication:AzureAd:AADInstance"] + Configuration["Authentication:AzureAd:TenantId"],
            //    CallbackPath = Configuration["Authentication:AzureAd:CallbackPath"],
            //    ResponseType = OpenIdConnectResponseType.IdToken
            //});

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");

                routes.MapRoute(
                    name: "DeployExists",
                    template: "{controller=TennantParams}/{action=IndexSelected}/{id}/{force}");
                    
              });

        }
    }
}
