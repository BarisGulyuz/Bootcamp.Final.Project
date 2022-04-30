using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teleperformance.Business.Extensions;
using Teleperformance.Core.Utilities.Mail;

namespace Teleperformance.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));

            services.AddApplicationDependencies(Configuration.GetConnectionString("Local"));

            //services.AddSession();

            services.ConfigureApplicationCookie(opt =>
            {
                opt.LoginPath = new PathString("/Auth/Login");
                opt.LogoutPath = new PathString("/Auth/Logout");
                opt.Cookie = new CookieBuilder
                {
                    Name = "Teleperformance",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest, //Always
                };
                opt.SlidingExpiration = true;
                opt.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                opt.AccessDeniedPath = new PathString("/Auth/AccessDenied");
            });
            services.AddControllersWithViews();

        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseSession();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
