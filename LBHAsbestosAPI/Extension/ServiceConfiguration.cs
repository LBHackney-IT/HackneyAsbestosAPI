using System;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Repositories;
using LBHAsbestosAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LBHAsbestosAPI.Extension
{
    public static class ServiceConfiguration
    {
		public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IPsi2000Api, Psi2000Api>();
            services.AddScoped(typeof(IAsbestosService), typeof(AsbestosService));
        }
    }
}
