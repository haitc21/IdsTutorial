using IdentityExample.Data;
using IdentityExample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;

namespace IdentityExample
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(config =>
           {
               config.UseInMemoryDatabase("Memory");
           });
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequireUppercase = false;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequiredLength = 3;
                config.Password.RequireLowercase = false;

                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Mặc định Asp Identity nó sẽ trả về cookie có tên AspNetCore.Identity.Application
            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "HaiDz.Identity.Cookie";
                config.LoginPath = "/home/login";
            });

            //var mailConfi = _configuration.GetSection("MailSettings").Get<MailKitOptions>();
            //services.AddMailKit(config => {
            //    config.UseMailKit(mailConfi);
            //});

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
            services.AddTransient<IEmailSender,EmailSenderService>();

            // Options
            services.AddOptions();                                        // Kích hoạt Options
            var mailsettings = _configuration.GetSection("MailSettings");  // đọc config
            services.Configure<MailSettings>(mailsettings);
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