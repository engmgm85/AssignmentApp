using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AssignmentAppDAL;
using AssignmentAppDAL.Services;
using Microsoft.EntityFrameworkCore;
using AssignmentAppDAL.Repositories;

namespace AssignmentAppAPI
{
    public class Startup
    {
        private const string MYCORSPOLICY = "All";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(MYCORSPOLICY, build => build.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()));
            services.AddControllers();
            services.AddDbContext<AssignmentAppDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AssignmentAppData")).EnableSensitiveDataLogging()
                 .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISurveyService, SurveyService>();
            services.AddTransient<IHearSourceService, HearSourceService>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AssignmentAppAPI", Version = "v1" });
            });
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AssignmentAppAPI v1"));
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(MYCORSPOLICY);


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
