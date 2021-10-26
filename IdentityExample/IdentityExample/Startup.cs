using IdentityExample.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityExample
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>( config =>
            {
                config.UseInMemoryDatabase("Memory");
            });
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //services.AddAuthentication("HaiDz")
            //    .AddCookie("HaiDz", config =>
            //    {
            //        // Name đặt thế nào cũng được
            //        // không có dấu cáCh
            //        config.Cookie.Name = "Haidz.Cookie";

            //        // Cho cookie nó sống trong 1 phút
            //        config.Cookie.MaxAge = new TimeSpan(0, 1, 0);


            //        // Nếu không cấu hình thì nó sẽ mặc định vào 
            //        // https://localhost:5001/Account/Login?ReturnUrl=%2Fhome%2Fsecret
            //        // Để login.
            //        // mà projct h làm gì có trang đấy nên nó lỗi thôi

            //        // cấu hình trang login
            //        config.LoginPath = "/home/authenticate";
            //    });

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
