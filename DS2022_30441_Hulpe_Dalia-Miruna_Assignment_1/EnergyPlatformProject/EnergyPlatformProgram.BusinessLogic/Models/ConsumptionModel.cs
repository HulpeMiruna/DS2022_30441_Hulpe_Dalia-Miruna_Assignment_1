using EnergyPlatform.Repository.Entitys;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyPlatformProgram.BusinessLogic.Models
{
    public class ConsumtionModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid DeviceId { get; set; }

        public float Consumtion { get; set; }
    }
}
