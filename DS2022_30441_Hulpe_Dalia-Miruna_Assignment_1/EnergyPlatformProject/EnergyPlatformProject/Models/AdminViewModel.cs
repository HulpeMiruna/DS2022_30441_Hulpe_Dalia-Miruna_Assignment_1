using EnergyPlatform.Repository.Entitys;
using System.Collections.Generic;

namespace EnergyPlatformProject.Models
{
    public class AdminViewModel
    {
        public IList<UserEntity> Users { get; set; }

        public List<DeviceViewModel> Devices { get; set; }
    }
}
