using EnergyPlatformProgram.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnergyPlatformProgram.Repository.Interfaces
{
    public interface IConsumtionRepository
    {
        Task<List<ConsumptionEntity>> GetDeviceConsumtionAsync(Guid deviceId, DateTime date);
    }
}
