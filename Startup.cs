using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Coba_Net.Data;
using Coba_Net.Middlewares;

namespace Coba_Net
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDb>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews();
            services.AddTransient<InitDb>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var InitDb = serviceScope.ServiceProvider.GetService<InitDb>();
                InitDb.Initialize();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            var protectedRoutes = new List<string> 
            { 
               "/", "/Home", "/Home/index", "/Home/Privacy", "/Car", "/Car/Index", "/Car/Add", "/Car/Edit" 
            };

            var unprotectedRoutes = new List<string> { "/User/Login" };

            app.UseWhen(context => protectedRoutes.Contains(context.Request.Path), builder =>
            {
                builder.UseMiddleware<Session>();
                builder.UseAuthorization();
            });

            app.UseWhen(context => unprotectedRoutes.Contains(context.Request.Path), builder =>
            {
                builder.UseMiddleware<EmptySession>();
                builder.UseAuthorization();
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
