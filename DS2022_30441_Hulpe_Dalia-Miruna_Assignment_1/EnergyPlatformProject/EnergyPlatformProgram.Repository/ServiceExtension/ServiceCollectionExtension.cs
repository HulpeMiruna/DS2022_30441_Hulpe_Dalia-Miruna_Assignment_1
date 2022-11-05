using EnergyPlatformProgram.Repository.Implementations;
using EnergyPlatformProgram.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyPlatformProgram.Repository.ServiceExtension
{
    public static class ServiceCollectionExtension
    {
        public static void AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IConsumtionRepository, ConsumtionRepository>();
        }
    }
}
