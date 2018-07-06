using System;
using LBHAsbestosAPI.Actions;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Logging;
using LBHAsbestosAPI.Repositories;
using LBHAsbestosAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LBHAsbestosAPI.Extension
{
    public static class ServiceConfiguration
    {
		public static void AddCustomServices(this IServiceCollection services)
		{
            services.AddScoped(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            services.AddTransient<IPsi2000Api, Psi2000Api>();
            services.AddScoped(typeof(IAsbestosService), typeof(AsbestosService));
        }
    }
}
