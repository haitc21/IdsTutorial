using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    var secretBytes = Encoding.UTF8.GetBytes(Constants.Secret);

                    // https://docs.microsoft.com/en-us/dotnet/api/system.identitymodel.tokens.symmetricsecuritykey?view=dotnet-plat-ext-5.0
                    var key = new SymmetricSecurityKey(secretBytes);

                    opt.Events = new JwtBearerEvents()
                    {
                        OnMessageReceived = (MessageReceivedContext context) =>
                        {
                            if (context.Request.Query.ContainsKey("access_token"))
                            {
                                context.Token = context.Request.Query["access_token"];
                            }
                            return Task.CompletedTask;
                        }
                    };

                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = Constants.Issuer,
                        ValidAudience = Constants.Audiance,
                        IssuerSigningKey = key
                    };
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
