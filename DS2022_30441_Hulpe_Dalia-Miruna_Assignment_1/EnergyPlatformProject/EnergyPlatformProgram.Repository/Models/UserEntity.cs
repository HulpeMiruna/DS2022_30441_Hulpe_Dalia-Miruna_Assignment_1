using EnergyPlatformProgram.Repository.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EnergyPlatform.Repository.Entitys
{
    public class UserEntity : IdentityUser<Guid>
    {
        public string FirstName { set; get; }

        public string LastName { set; get; }

        public List<DeviceEntity> Devices { get; set; }
    }
}
