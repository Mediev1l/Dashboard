using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBoard.DataAccess.Data;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using MyBoard.Models.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.AspNetCore.Routing.Template;
using MyBoard.Repository.IRepository;
using MyBoard.Repository;
using NETCore.MailKit.Extensions;
using NETCore.MailKit.Infrastructure.Internal;
using Microsoft.AspNetCore.Identity;
using MailKit.Security;
using MyBoard.Models.Email;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyBoard
{
    public class Startup
    { 

        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
    
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;

                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 4;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "LoggedUser";
                config.LoginPath = "/Account/Login";
            });

            services.AddAuthorization(options => {
                options.AddPolicy("Admin", claim => claim.RequireClaim("Admin", "true"));
            }); 

            services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseSqlServer(_config.GetConnectionString("AppData"));
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IEmailConfiguration>(_config.GetSection("Email").Get<EmailConfiguration>());
            services.AddTransient<IEmailService, EmailService>();

            services.AddControllersWithViews();
            //SecureSocketOptions.Auto
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
                app.UseExceptionHandler("Dashboard/Error");
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
                    pattern : "{controller=Dashboard}/{action=Index}/{id?}");
            });
        }
    }
}
