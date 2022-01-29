using Admin.Data;
using Entities;
using Infrastructure;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Admin
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _config;

        public Startup(IWebHostEnvironment env)
        {
            _env = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(_env.ContentRootPath)
                .AddJsonFile($"appsettings.json");

            _config = builder.Build();
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_config);
            services.AddMvc();

            services.AddDbContext<AskDbContext>();
            services.AddTransient<DataSeeder>();

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AskDbContext>();


            services.Configure<IdentityOptions>(config =>
            {
                // Password settings
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequiredLength = 8;
                config.Password.RequiredUniqueChars = 0;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;

                // User settings
                config.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                config.User.RequireUniqueEmail = true;

                // Lockout settings
                config.Lockout.AllowedForNewUsers = false;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                config.Lockout.MaxFailedAccessAttempts = 3;
            });

            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
                config.AccessDeniedPath = "/Auth/AccessDenied";

                config.Cookie.HttpOnly = true;
                config.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                config.SlidingExpiration = true;


                config.ReturnUrlParameter = "returnUrl";
            });

            services.AddTransient<IEFRepository, EFRepository>();
            services.AddAuthorization();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            if (_env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error/NotFound");
                app.UseHsts();
            }

            //app.UseStatusCodePages();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");
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

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
