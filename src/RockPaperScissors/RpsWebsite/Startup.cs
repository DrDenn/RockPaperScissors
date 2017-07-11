using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RpsWebsite.Entities;
using Microsoft.Extensions.Configuration;
using RpsWebsite.Services;

namespace RpsWebsite
{
    public class Startup
    {
        private IConfiguration _config;

        /// <summary>
        /// ASP.NET will provide the HostingEnvironment to the ctor.
        /// Constructs the system configuration.
        /// </summary>
        /// <param name="env">The hosting environment.</param>
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                            .SetBasePath(env.ContentRootPath)
                            .AddJsonFile("appsettings.json")
                            .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true)
                            .AddEnvironmentVariables();

            _config = builder.Build();
        }

        /// <summary>
        /// Called by the runtime to setup middleware.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable ASP.NET MVC
            services.AddMvc();

            // Enable ASP.NET EntityFramework Identity Service (for logging in & auth-n)
            services.AddDbContext<RpcUserDb>(options => options.UseInMemoryDatabase());
            services.AddIdentity<User, IdentityRole>(options =>
                {
                    // - low security passwords -
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredLength = 6;
                }).AddEntityFrameworkStores<RpcUserDb>();

            // Add the Configuration as a convenience middleware
            services.AddSingleton(_config);

            // Add the RpsServer client service
            services.AddSingleton<IRpsServerClient, RpsServerClient>();
        }

        /// <summary>
        /// Called by the runtime to setup the request routing pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions()
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Whoops. This isn't supposed to happen. Please try again!")
                });
            }

            app.UseFileServer(false);

            app.UseIdentity();

            app.UseMvc(builder => builder.MapRoute("Default", "{controller=Home}/{action=Index}"));
        }
    }
}
