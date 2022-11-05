using System;
using System.Collections.Generic;
using System.Text;

namespace EnergyPlatformProgram.Repository.Models
{
    public class ConsumptionEntity
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid DeviceId { get; set; }

        public float Consumtion { get; set; }
    }
}
