using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.Repository.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyPlatformProgram.Repository.Models
{
    public class DeviceEntity : IEntity
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public string Address { get; set; }

        public string MaximuHourlyEnergyConsumtion { get; set; }

        public UserEntity Owner { get; set; }
    }
}
