using Basics.AuthorizationRequirements;
using Basics.Tranformer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Basics
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("HaiDz")
                .AddCookie("HaiDz", config =>
                {
                    // Name đặt thế nào cũng được
                    // không có dấu cáCh
                    config.Cookie.Name = "Haidz.Cookie";

                    // Cho cookie nó sống trong 1 phút
                    //config.Cookie.MaxAge = new TimeSpan(0, 1, 0);


                    // Nếu không cấu hình thì nó sẽ mặc định vào 
                    // https://localhost:5001/Account/Login?ReturnUrl=%2Fhome%2Fsecret
                    // Để login.
                    // mà projct h làm gì có trang đấy nên nó lỗi thôi

                    // cấu hình trang login
                    config.LoginPath = "/home/authenticate";
                    config.AccessDeniedPath = "/home/AccessDenie";
                });

            services.AddAuthorization(config =>
            {
                // 3 cách để khai báo policy

                //var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                //var defaultPolicy = defaultAuthBuilder
                //                    .RequireAuthenticatedUser()
                //                    .RequireClaim(ClaimTypes.DateOfBirth)
                //                    .Build();

                //config.DefaultPolicy = defaultPolicy;

                //config.AddPolicy("NgaySinh", policyBuilder =>
                //{
                //    policyBuilder.AddRequirements(new CustomRequireClaim(ClaimTypes.DateOfBirth));
                //});

                config.AddPolicy("NgaySinh", policyBuilder =>
                {
                    policyBuilder.RequireCustomClaim(ClaimTypes.DateOfBirth);
                });

                //==========
                config.AddPolicy("NhomQuyen", policyBuilder =>
                {
                    policyBuilder.RequireClaim(ClaimTypes.Role, "Admin");
                });

            });

            services.AddControllersWithViews(config =>
            {

                var defaultAuthBuilder = new AuthorizationPolicyBuilder();
                var defaultPolicy = defaultAuthBuilder
                                    .RequireAuthenticatedUser()
                                    .RequireClaim(ClaimTypes.DateOfBirth)
                                    .Build();
                // global authorizefilter
                // Mặc định tất cả controller đều phải tuần theo defaultPolicy
                // Tự động redirect về trang đăng nhập cấu hình ở AddAuthentication
                //config.Filters.Add(new AuthorizeFilter(defaultPolicy));
            });


            services.AddScoped<IAuthorizationHandler, CookieJarAuthorizationHandler>();
            services.AddScoped<IAuthorizationHandler, CookieJarAuthorizationHandler>();
            services.AddScoped<IClaimsTransformation, ClaimTranformation>();
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
