using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mission9Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission9Assignment
{
    //Josh Blad
    //Assignment 9
    //Section 4
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration temp)
        {
            Configuration = temp;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<BookStoreContext>(options =>
            {
                options.UseSqlite(Configuration["ConnectionStrings:BookstoreDBConnection"]);
            });

            services.AddScoped<IBookStoreRepository, EFBookStoreRepository>();

            services.AddRazorPages();

            services.AddDistributedMemoryCache();

            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();

            //Using endpoints to make better formatting for URLs
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("categorypage",
                    "{category}/Page{pageNum}",
                    new { Controller = "Home", action = "Index" });

                endpoints.MapControllerRoute(name: "Paging",
                    pattern: "Page{pageNum}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapControllerRoute("category",
                    "{category}",
                    new { Controller = "Home", action = "Index", pageNum = 1 });

                endpoints.MapDefaultControllerRoute();

                endpoints.MapRazorPages();
            });
        }
    }
}
