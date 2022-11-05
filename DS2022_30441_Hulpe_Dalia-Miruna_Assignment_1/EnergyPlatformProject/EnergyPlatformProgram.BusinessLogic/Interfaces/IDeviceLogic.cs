using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnergyPlatformProgram.BusinessLogic.Interfaces
{
    public interface IDeviceLogic
    {
        Task AddAsync(DeviceModel department);

        Task<List<DeviceModel>> GetAllAsync();

        Task<DeviceModel> FindByIdAsync(Guid id);

        void UpdateDevice(DeviceModel newDepartmentModel);

        Task DeleteDeviceAsync(Guid id);

        Task AlocateOwner(Guid id, UserEntity user);

        Task<List<DeviceModel>> GetByOwnerIdAsync(Guid id);

        Task<List<ConsumtionModel>> GetConsumtionForDeviceAsync(Guid id, DateTime date);
    }
}
