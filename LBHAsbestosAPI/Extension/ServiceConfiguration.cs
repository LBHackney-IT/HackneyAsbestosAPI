using System;
using LBHAsbestosAPI.Interfaces;
using LBHAsbestosAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace LBHAsbestosAPI.Extension
{
    public static class ServiceConfiguration
    {
		public static void AddCustomServices(this IServiceCollection services)
        {
			services.AddSingleton<IAsbestosService, AsbestosService>();
        }
    }
}
