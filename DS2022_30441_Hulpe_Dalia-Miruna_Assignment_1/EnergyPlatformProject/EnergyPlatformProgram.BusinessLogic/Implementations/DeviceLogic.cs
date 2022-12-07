using AutoMapper;
using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.BusinessLogic.Interfaces;
using EnergyPlatformProgram.BusinessLogic.Models;
using EnergyPlatformProgram.Repository.Interfaces;
using EnergyPlatformProgram.Repository.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyPlatformProgram.BusinessLogic.Implementations
{
    public class DeviceLogic : IDeviceLogic
    {
        public readonly IDeviceRepository _deviceRepository;
        public readonly IMapper _mapper;
        public readonly IUserRepository _userRepository;
        private readonly IConsumtionRepository _consumtionRepository;

        public DeviceLogic(IDeviceRepository deviceRepository, IMapper mapper, IUserRepository userRepository, IConsumtionRepository consumtionRepository)
        {
            _deviceRepository = deviceRepository;
            _mapper = mapper;
            _userRepository = userRepository;
            _consumtionRepository = consumtionRepository;
        }

        public async Task AddAsync(DeviceModel device)
        {
            var deviceEntity = _mapper.Map<DeviceEntity>(device);

            await _deviceRepository.AddAsync(deviceEntity);
        }

        public async Task AlocateOwner(Guid id, UserEntity owner)
        {
            var device = await _deviceRepository.GetByIdAsync(id);

            device.Owner = owner;

            _deviceRepository.Update(device);
        }

        public async Task<List<DeviceModel>> GetAllAsync()
        {
            var devicesEntitys = await _deviceRepository.GetAllWithOwnerAsync();

            var devices = devicesEntitys.Select(d => _mapper.Map<DeviceModel>(d)).ToList();

            return devices;
        }

        public async Task<DeviceModel> FindByIdAsync(Guid id)
        {
            var deviceEntity = await _deviceRepository.GetAsync(id);

            var deviceModel = _mapper.Map<DeviceModel>(deviceEntity);

            return deviceModel;
        }

        public void UpdateDevice(DeviceModel newDeviceModel)
        {
            var deviceEntity = _mapper.Map<DeviceEntity>(newDeviceModel);

            var entity = _deviceRepository.Update(deviceEntity);
        }

        public async Task DeleteDeviceAsync(Guid id)
        {
            await _deviceRepository.DeleteAsync(id);
        }

        public async Task<List<DeviceModel>> GetByOwnerIdAsync(Guid id)
        {
            var deviceEntitys = await _deviceRepository.GetByOwnerIdAsync(id);

            var devices = deviceEntitys.Select(d => _mapper.Map<DeviceModel>(d)).ToList();

            return devices;
        }

        public async Task<List<ConsumtionModel>> GetConsumtionForDeviceAsync (Guid id, DateTime date)
        {
            var consumptionEntitys = await _consumtionRepository.GetDeviceConsumtionAsync(id, date);

            var consumption = consumptionEntitys.Select(c => _mapper.Map<ConsumtionModel>(c)).ToList();

            return consumption;
        }

        public async Task AddConsumtionAsync(ConsumtionModel consumtion)
        {
            var consumtionEntity = _mapper.Map<ConsumptionEntity>(consumtion);

            await _consumtionRepository.AddConsumtionAsync(consumtionEntity);
        }
    }
}
