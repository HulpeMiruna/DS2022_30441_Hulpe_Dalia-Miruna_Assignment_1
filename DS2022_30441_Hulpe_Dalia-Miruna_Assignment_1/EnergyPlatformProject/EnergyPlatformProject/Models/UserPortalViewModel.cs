using EnergyPlatform.Repository.Entitys;
using System;
using System.Collections.Generic;

namespace EnergyPlatformProject.Models
{
    public class UserPortalViewModel
    {
        public DateTime Date { get; set; }

        public List<DeviceViewModel> Devices { get; set; }
    }
}
