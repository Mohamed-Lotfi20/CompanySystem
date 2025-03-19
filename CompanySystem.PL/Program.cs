using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using CompanySystem.BLL.Interfaces;
using CompanySystem.BLL.Repositories;
using CompanySystem.DAl.Contexts;
using CompanySystem.DAl.Models;
using CompanySystem.PL.MapperProfiles;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanySystem.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            #region Configration Services and Allow Dependancy Injection

            builder.Services.AddControllersWithViews();

            #region To Connection by DataBase
            builder.Services.AddDbContext<CompanyDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            #endregion

            builder.Services.AddScoped<IUnitOfWork, UnitOFWork>();

            #region To Add Profile
            builder.Services.AddAutoMapper(M => M.AddProfile(new EmployeeProfile()));
            builder.Services.AddAutoMapper(D => D.AddProfile(new DepartmentProfile()));
            builder.Services.AddAutoMapper(D => D.AddProfile(new UserProfile()));
            builder.Services.AddAutoMapper(D => D.AddProfile(new RoleProfile()));
            #endregion

            #region To Create User
            //1. services.AddScoped<UserManager<ApplicationUser>>();
            //2. services.AddScoped<SignInManager<ApplicationUser>>();
            //3. services.AddScoped<RoleManager<ApplicationUser>>();
            /*(1,2,3) => */
            builder.Services.AddAuthentication();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 7;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            }).AddEntityFrameworkStores<CompanyDbContext>() // To Complete Fun=> Create In Account Register
            .AddDefaultTokenProviders(); // Default Token
            #endregion

            #region Create Cookies Schema 

            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "Account/Login";
                    options.AccessDeniedPath = "Home/Error";
                });
            #endregion

            #endregion


            var app = builder.Build();
            var env = builder.Environment;
            #region Configure HTTP Request Pipline
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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            #endregion

            app.Run();
        }
    }
}
