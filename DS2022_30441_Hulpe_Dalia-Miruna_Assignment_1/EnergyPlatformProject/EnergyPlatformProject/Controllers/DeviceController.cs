using AutoMapper;
using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.BusinessLogic.Constants;
using EnergyPlatformProgram.BusinessLogic.Interfaces;
using EnergyPlatformProgram.BusinessLogic.Models;
using EnergyPlatformProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EnergyPlatformProject.Controllers
{
    public class DeviceController : Controller
    {

        public readonly IDeviceLogic _deviceLogic;
        public readonly IMapper _mapper;
        private readonly UserManager<UserEntity> _userManager;

        public DeviceController(IDeviceLogic deviceLogic, IMapper mapper, UserManager<UserEntity> userManager)
        {
            _deviceLogic = deviceLogic;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Authorize(Policy = RoleConstants.AdminRequirement)]
        [HttpGet]
        public async Task<IActionResult> AddDevice()
        {
            return View();
        }

        [Authorize(Policy = RoleConstants.AdminRequirement)]
        [HttpPost]
        public async Task<IActionResult> AddDevice(DeviceViewModel deviceViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(deviceViewModel);
            }

            var deviceModel = _mapper.Map<DeviceModel>(deviceViewModel);

            await _deviceLogic.AddAsync(deviceModel);

            return RedirectToAction("AdminPortal", "Account");
        }

        [Authorize(Policy = RoleConstants.AdminRequirement)]
        [HttpGet]
        public async Task<IActionResult> UpdateDevice(Guid id)
        {
            var deviceViewModel = new DeviceViewModel();

            var deviceModel = await _deviceLogic.FindByIdAsync(id);

            deviceViewModel.Id = deviceModel.Id;
            deviceViewModel.Description = deviceModel.Description;
            deviceViewModel.Address = deviceModel.Address;
            deviceViewModel.MaximuHourlyEnergyConsumtion = deviceModel.MaximuHourlyEnergyConsumtion;
            deviceViewModel.Owner = deviceModel.Owner;

            return View(deviceViewModel);
        }

        [Authorize(Policy = RoleConstants.AdminRequirement)]
        [HttpPost]
        public IActionResult UpdateDevice(DeviceViewModel deviceViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(deviceViewModel);
            }

            var deviceModel = new DeviceModel()
            {
                Id = deviceViewModel.Id,
                Address = deviceViewModel.Address,
                Description = deviceViewModel.Description,
                Owner = deviceViewModel.Owner,
                MaximuHourlyEnergyConsumtion = deviceViewModel.MaximuHourlyEnergyConsumtion
            };

            _deviceLogic.UpdateDevice(deviceModel);

            return RedirectToAction("AdminPortal", "Account");
        }

        [Authorize(Policy = RoleConstants.AdminRequirement)]
        [HttpGet]
        public async Task<IActionResult> DeleteDevice(Guid id)
        {
            await _deviceLogic.DeleteDeviceAsync(id);

            return RedirectToAction("AdminPortal", "Account");
        }

        [Authorize(Policy = RoleConstants.AdminRequirement)]
        [HttpGet]
        public async Task<IActionResult> AlocateOwner (Guid id)
        {
            var userEmails = await _userManager.GetUsersInRoleAsync(RoleConstants.UserRole);
            var deviceAddViewModel = new DeviceAddOwnerViewModel()
            {
                DeviceId = id,
                UserEmails = userEmails.Select(u => new SelectListItem { Value = u.Email, Text = u.Email }).ToList()
        };

            return View(deviceAddViewModel);
        }

        [Authorize(Policy = RoleConstants.AdminRequirement)]
        [HttpPost]
        public async Task<IActionResult> AlocateOwner(DeviceAddOwnerViewModel deviceAddViewModel)
        {
            var owner = await _userManager.FindByEmailAsync(deviceAddViewModel.OwnerEmail);

            await _deviceLogic.AlocateOwner(deviceAddViewModel.DeviceId, owner);

            return RedirectToAction("AdminPortal", "Account");
        }

        [Authorize(Policy = RoleConstants.UserRequirement)]
        [HttpGet]
        public async Task<IActionResult> Consumption(Guid id, DateTime? date)
        {
            if (date == null)
            {
                date = DateTime.Now;
            }

            var consumption = await _deviceLogic.GetConsumtionForDeviceAsync(id, date.Value);
            var consumptionHour = consumption.Select(c => (int)c.Date.Hour).ToList();
            var consumptionValue = consumption.Select(c => (int)c.Consumtion).ToList();

            var model = new ConsumptionViewModel()
            {
                Id = id,
                Date = date.Value,
                ConsumptionHour = consumptionHour,
                ConsumptionValue = consumptionValue
            };

            return View(model);
        }

        [Authorize(Policy = RoleConstants.UserRequirement)]
        [HttpPost]
        public async Task<IActionResult> Consumption(ConsumptionViewModel model)
        {
            return RedirectToAction("Device", "Consumption", new { id = model.Id, date = model.Date});
        }
    }
}
