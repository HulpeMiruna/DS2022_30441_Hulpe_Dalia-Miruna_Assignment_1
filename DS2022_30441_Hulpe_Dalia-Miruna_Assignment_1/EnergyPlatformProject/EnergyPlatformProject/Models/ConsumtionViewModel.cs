using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.BusinessLogic.Models;
using System;
using System.Collections.Generic;

namespace EnergyPlatformProject.Models
{
    public class ConsumptionViewModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public List<int> ConsumptionHour { get; set; }

        public List<int> ConsumptionValue { get; set; }
    }
}
