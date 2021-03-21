/*******************************************************************
 * Team 2: Jason Thomas | Travis Johnson
 * 12-7-2020
 * "Final Project (Team)"
 * "complete a CRUD MVC ASP.NET core application"
 *******************************************************************/

using MedicationList.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity; // Using directive for Identity namespace

namespace MedicationList
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
            services.AddMemoryCache(); // Add memory cache for session state
            services.AddSession(); // Add session state

            services.AddControllersWithViews().AddNewtonsoftJson(); // Use Newtonsoft JSON library
            services.AddDbContext<MedicationContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MedicationContext"))); // Enable injection of DbContext objects
            services.AddRouting(options => {
                options.LowercaseUrls = true; // Make URLs lowercase
                options.AppendTrailingSlash = true; // Add slash to end of URL
            });

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<MedicationContext>()
                .AddDefaultTokenProviders(); // Add Identity service w/ default password
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

            app.UseAuthentication(); // activate use of authentication
            app.UseAuthorization(); // activate use of authorization

            app.UseSession(); // activate use of session state

            app.UseEndpoints(endpoints =>
            {
                // Route to Admin area
                endpoints.MapAreaControllerRoute(
                    name: "admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");
                // Route to default area
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}/{slug?}");
            });

            MedicationContext.CreateAdminUser(app.ApplicationServices).Wait(); // Call method for creating admin user
        }
    }
}