using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace oauth
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("oauth")
                .AddJwtBearer("oauth", (JwtBearerOptions opt) => 
                { 

                });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            // Who you are? Mày là ai?
            app.UseAuthentication();

            // are you allowed? Mày có được phép truy cập k?
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                // MVC
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
