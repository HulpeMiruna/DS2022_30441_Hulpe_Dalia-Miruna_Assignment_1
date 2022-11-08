using EnergyPlatform.BusinessLogic.Data;
using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.BusinessLogic.Constants;
using EnergyPlatformProgram.BusinessLogic.Implementations;
using EnergyPlatformProgram.BusinessLogic.ServiceExtension;
using EnergyPlatformProject.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyPlatformProject
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection"),
                    options => options.EnableRetryOnFailure()));

            services.AddAutoMapper(typeof(Startup));
            services.AddDefaultIdentity<UserEntity>()
                 .AddRoles<IdentityRole<Guid>>()
                 .AddRoleManager<RoleManager<IdentityRole<Guid>>>()
                 .AddEntityFrameworkStores<ApplicationDbContext>()
                 .AddClaimsPrincipalFactory<UserClaimsFactory>();
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddBusinessLogic();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(50);

                options.AccessDeniedPath = "/Home/AccessDenied";
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.SlidingExpiration = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(RoleConstants.UserRequirement,
                     policy => policy.RequireRole(RoleConstants.UserRole));
                options.AddPolicy(RoleConstants.AdminRequirement,
                     policy => policy.RequireRole(RoleConstants.AdminRole));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
