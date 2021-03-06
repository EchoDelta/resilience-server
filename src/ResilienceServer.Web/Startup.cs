﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResilienceServer.Web.Options;
using ResilienceServer.Web.ResilienceServices;
using Swashbuckle.AspNetCore.Swagger;

namespace ResilienceServer.Web
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Resilience Server",
                    Description = "HTTP server for testing resilience libraries."
                });
            });
            services.Configure<ResilienceOptions>(Configuration.GetSection("Resilience"));

            services.AddSingleton<IMightFailResilienceService, MightFailResilienceService>();
            services.AddSingleton<IWaitForItResilienceService, WaitForItResilienceService>();
            services.AddSingleton<IFragileResilienceService, FragileResilienceService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Resilience Server V1");
            });
        }
    }
}
