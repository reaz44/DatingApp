using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API
{
    public class Startup
    {
        private readonly IConfiguration _config;
       //When the Startup class is constructed, the IConfiguration is being injected into the class 
       //through the constructor. that means, we will have access to this configuration wherever we 
       //need it.
        public Startup(IConfiguration config)
        {
            _config = config;
           
        }

        //public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container from other areas.
        //This is also called a dependency injection container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
           {
               options.UseSqlite(_config.GetConnectionString("DefaultConnection"));
           });
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // As we make our GET request from our browser to the controller endpoint, the request goes through 
        // a series of middleware on the way in and also on the way out.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //At first we check, if we are in development mode. If we are then we use the development exception page
            // as well as swagger over here
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }


            //if we encounter http, then we get redirection to https endpoint
            app.UseHttpsRedirection();

            //To route the request in the correct direction
            app.UseRouting();
            
            //UseCors must be between routing and Endpoint and also prior to authorization and authentication
            app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"));

            app.UseAuthorization();

            // we have middleware to use the endpoints and to map the controllers
            app.UseEndpoints(endpoints =>
            {
                //this sees inside the controllers for endpoints
                endpoints.MapControllers();
            });
        }
    }
}
