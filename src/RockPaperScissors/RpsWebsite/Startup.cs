using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RpsWebsite.Entities;

namespace RpsWebsite
{
    public class Startup
    {
        /// <summary>
        /// Called by the runtime to setup middleware.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable ASP.NET MVC
            services.AddMvc();

            // Enable ASP.NET EntityFramework Identity Service (for logging in & auth-n)
            services.AddDbContext<RpcUserDb>(options => options.UseInMemoryDatabase());
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<RpcUserDb>();
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
