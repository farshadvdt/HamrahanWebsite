using Contexts;
using HamrahanTemplate.Infrastructure.UnitOfWork;
using HamrahanWebsite.Areas;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Hamrahan.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Identity.Web;
using System.Security.Claims;
using Microsoft.Identity.Client;
using HamrahanWebsite.Areas.Identity;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace HamrahanWebsite
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
            services.AddControllersWithViews();
            services.AddDbContext<HamrahanDbContext>(option =>
            option.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            .UseQueryTrackingBehavior( QueryTrackingBehavior.NoTracking)); //just for not tracking queries for better performance
            services.AddIdentity<Person, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequireDigit = false;
                option.Password.RequiredLength = 8;
                option.Password.RequireNonAlphanumeric = false;
               
                
            }).AddEntityFrameworkStores<HamrahanDbContext>().AddDefaultTokenProviders();
            services.AddAuthorization(option =>
            {
                option.AddPolicy("WatchPosts", policy => policy.RequireClaim("Watch Post"));
            });
            
            //services.AddAuthentication(option =>
            //{
            //    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //}).AddCookie(option =>
            //{

            //    //option.LoginPath = "/Auth/Login";
            //    //option.LogoutPath = "/Auth/Logout";
            //    //option.AccessDeniedPath = "/Auth/AccessDenied";
            //    option.ExpireTimeSpan = TimeSpan.FromDays(30);
            //});
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddScoped<IUow, Uow>();
            services.AddSingleton<IEmailSender, EmailService>();
            //for adding tempdata
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            
        

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
            
            app.UseAuthentication();

            app.UseAuthorization();
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

               
            

         
            
        

               
            });

        }
    }
}
