using EnergyPlatform.Repository.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyPlatformProgram.BusinessLogic.Models
{
    public class DeviceModel
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string MaximuHourlyEnergyConsumtion { get; set; }

        public UserEntity Owner { get; set; }
    }
}
