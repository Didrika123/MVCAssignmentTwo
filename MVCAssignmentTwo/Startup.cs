using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCAssignmentTwo.Models;
using MVCAssignmentTwo.Models.Data;
using MVCAssignmentTwo.Models.Identity;
using MVCAssignmentTwo.Models.Services;

namespace MVCAssignmentTwo
{
    public class Startup
    {
        private readonly IConfiguration Configuration;  //Configuration strings you find in Appsettings.json
        public Startup(IConfiguration config) { Configuration = config; }
        public void ConfigureServices(IServiceCollection services)
        {
            // For Identity
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<RegisterDbContext>()
                .AddDefaultTokenProviders();

            // For api, so that we can allow cross-origin-r-somehting  (aka allow other than host to access it)
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowAnyOrigin();
                   //.AllowCredentials();
            }));

            /*
             * There are different types of injection 
             * - Transient: Recreated each time requested from the Service
             * - Scoped:    Created one per client connection
             * - Singleton: Lifetime of the program
            */

            //services.AddSingleton<IPeopleRepo, InMemoryPeopleRepo>();
            services.AddDbContext<RegisterDbContext>(option => option
                .UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
                //.EnableSensitiveDataLogging()
            );

            services.AddScoped<IPeopleRepo, DatabasePeopleRepo>();
            services.AddScoped<ICitiesRepo, DatabaseCitiesRepo>();
            services.AddScoped<ICountriesRepo, DatabaseCountriesRepo>();
            services.AddScoped<ILanguagesRepo, DatabaseLanguagesRepo>();

            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<ILanguagesService, LanguagesService>();
            services.AddMvc().AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy"); // For api

            // For identity
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
