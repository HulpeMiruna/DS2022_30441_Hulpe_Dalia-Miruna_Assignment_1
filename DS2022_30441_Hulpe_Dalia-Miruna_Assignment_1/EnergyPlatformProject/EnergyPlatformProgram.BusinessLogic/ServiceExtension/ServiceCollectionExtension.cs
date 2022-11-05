using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.BusinessLogic.Implementations;
using EnergyPlatformProgram.BusinessLogic.Interfaces;
using EnergyPlatformProgram.Repository.ServiceExtension;
using EnergyPlatformProject.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace EnergyPlatformProgram.BusinessLogic.ServiceExtension
{
    public static class ServiceCollectionExtension
    {
        public static void AddBusinessLogic(this IServiceCollection services)
        {
            services.AddScoped<IDeviceLogic, DeviceLogic>();
            services.AddScoped<IUserLogic, UserLogic>();
            services.AddRepository();
        }
    }
}
