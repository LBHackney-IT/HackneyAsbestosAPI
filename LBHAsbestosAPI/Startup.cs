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
using System.Linq;
using System.Collections.Generic;

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
                c.AddSecurityDefinition("Token",
                  new ApiKeyScheme
                  {
                      In = "header",
                      Description = "Your Hackney API Key",
                      Name = "X-Api-Key",
                      Type = "apiKey"
                  });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Token", Enumerable.Empty<string>() }
                });

                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "LBH Abestos API - " + environment,
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

            app.UseMvc();

            string routePrefix = Environment.GetEnvironmentVariable("SWAGGER_ROUTE_PREFIX");
            string swaggerEndpoint = Environment.GetEnvironmentVariable("SWAGGER_ENDPOINT");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(swaggerEndpoint, "Hackney Repairs API");
                c.RoutePrefix = routePrefix;
            });
           
            app.UseDeveloperExceptionPage();
        }
    }
}
