using Barber.AppCode.Provider;
using Final_Project.Models.DataContext;
using Final_Project.Models.Entity.Membership;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Final_Project
{
    public class Startup
    {
        readonly IConfiguration configuration;
        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BarberDbContext>(cfg =>
            {

                cfg.UseSqlServer(configuration.GetConnectionString("cString"));
            });


            services.AddIdentity<BarberUser, BarberRole>()
                .AddEntityFrameworkStores<BarberDbContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<UserManager<BarberUser>>();
            services.AddScoped<SignInManager<BarberUser>>();
            services.AddScoped<RoleManager<BarberRole>>();

            services.Configure<IdentityOptions>(cfg =>
            {
                cfg.Password.RequireDigit = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequiredUniqueChars = 1;
                cfg.Password.RequireNonAlphanumeric = false;
                cfg.Password.RequiredLength = 3;


                cfg.User.RequireUniqueEmail = true;
                //cfg.user.allowedusernamecharacters = "";
                cfg.Lockout.MaxFailedAccessAttempts = 3;
                cfg.Lockout.DefaultLockoutTimeSpan = new TimeSpan(0, 3, 0);
            });
            services.ConfigureApplicationCookie(cfg =>
            {
                cfg.AccessDeniedPath = "/accessdenied.html";
                cfg.LoginPath = "/signin.html";
                cfg.ExpireTimeSpan = new TimeSpan(0, 5, 0);
                cfg.Cookie.Name = "riode";
            });
            services.AddAuthentication();
            services.AddAuthorization();

            services.AddScoped<UserManager<BarberUser>>();
            services.AddScoped<SignInManager<BarberUser>>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStaticFiles();
            // app.SeedMembership();


            app.UseRouting();

            app.Use(async (context, next) =>
            {
                if (!context.Request.Cookies.ContainsKey("riode")
                && context.Request.RouteValues.TryGetValue("area", out object areaName)
                && areaName.ToString().ToLower().Equals("admin"))
                {
                    var attr = context.GetEndpoint().Metadata.GetMetadata<AllowAnonymousAttribute>();
                    if (attr == null)
                    {

                        context.Response.Redirect("/admin/singin.html");
                        await context.Response.CompleteAsync();

                    }

                }
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.SeedMembership();


            app.UseRequestLocalization(cfg =>
            {
                cfg.AddSupportedUICultures("az", "en");
                cfg.AddSupportedCultures("az", "en");
                cfg.RequestCultureProviders.Clear();
                cfg.RequestCultureProviders.Add(new CultureProvider());


            });
            app.UseEndpoints(endpoints =>
            {


                endpoints.MapControllerRoute(

                    name: "Default-signin",
                    pattern: "accessdenied.html",
                    defaults: new
                    {
                        areas = "",
                        controller = "Home",
                        action = "accessdenied"
                    });



                endpoints.MapControllerRoute("default-with-lang", "{lang}/{controller=Home}/{action=index}/{id?}", constraints: new
                {
                    lang = "en|az|ru"
                });

                endpoints.MapControllerRoute("admin", "admin/signin.html",
                   defaults: new
                   {
                       controller = "Account",
                       action = "signin",
                       area = "Admin"
                   });

                endpoints.MapControllerRoute("x", "signin.html",
                    defaults: new
                    {
                        controller = "home",
                        action = "signin",
                        area = ""
                    });
                endpoints.MapControllerRoute("x", "register.html",
                    defaults: new
                    {
                        controller = "home",
                        action = "register",
                        area = ""
                    });
                endpoints.MapControllerRoute("logout.html", "admin/logout.html",
                    defaults: new
                    {
                        controller = "home",
                        action = "logout",
                        area = "Admin"
                    });


                endpoints.MapControllerRoute("adminsingin", "admin/singin.html",
                 defaults: new
                 {
                     controller = "Account",
                     action = "singin",
                     area = "Admin"
                 });



                endpoints.MapControllerRoute(
         name: "areas",
         pattern: "{area:exists}/{controller=Blog}/{action=Index}/{id?}"
       );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
