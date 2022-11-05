using AutoMapper;
using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.BusinessLogic.Interfaces;
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
    public class UserLogic : IUserLogic
    {
        public readonly IDeviceLogic _deviceLogic;
        public readonly IDeviceRepository _deviceRepository;
        public readonly UserManager<UserEntity> _userManager;
        public readonly IMapper _mapper;

        public UserLogic (IDeviceLogic deviceLogic, UserManager<UserEntity> userManager, IMapper mapper, IDeviceRepository deviceRepository)
        {
            _deviceLogic = deviceLogic;
            _userManager = userManager;
            _mapper = mapper;
            _deviceRepository = deviceRepository;
        }

        public async Task RemoveWithDevices (UserEntity user)
        {
            var devices = await _deviceRepository.GetByOwnerIdAsync(user.Id);

            await _deviceRepository.RemoveMultipleAsync(devices);

            await _userManager.DeleteAsync(user);

            foreach(var device in devices)
            {
                device.Owner = new UserEntity();
            }
        }
    }
}
