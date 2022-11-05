using EnergyPlatformProgram.Repository.Data;
using EnergyPlatformProgram.Repository.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnergyPlatformProgram.Repository.Interfaces
{
    public interface IDeviceRepository : IRepository<DeviceEntity>
    {
        DeviceEntity Update(DeviceEntity entity);

        Task<DeviceEntity> GetByIdAsync(Guid id);

        Task<List<DeviceEntity>> GetByOwnerIdAsync(Guid id);

        Task<List<DeviceEntity>> GetAllWithOwnerAsync();

        Task RemoveMultipleAsync(List<DeviceEntity> devices);

        Task AddMultipleAsync(List<DeviceEntity> devices);
    }
}
