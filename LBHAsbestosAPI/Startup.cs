using System;
using System.IO;
using System.Reflection;
using LBHAsbestosAPI.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using NLog.Extensions.Logging;
using NLog.Web;

namespace LBHAsbestosAPI
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
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "LBH Abestos API",
                    TermsOfService = "None"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            if (TestStatus.IsRunningIntegrationTests)
            {
                services.AddCustomTestServices();
            }
            else
            {
                services.AddCustomServices();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {         
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            loggerFactory.AddNLog();
            app.AddNLogWeb();
			
            if (!TestStatus.IsRunningIntegrationTests)
            {
                env.ConfigureNLog("NLog.config");
            }
            loggerFactory.AddNLog();
            app.AddNLogWeb();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI( cw =>
            {
                cw.SwaggerEndpoint("/swagger/v1/swagger.json", "LBH Abestos API v1");
            });
        }
    }
}
