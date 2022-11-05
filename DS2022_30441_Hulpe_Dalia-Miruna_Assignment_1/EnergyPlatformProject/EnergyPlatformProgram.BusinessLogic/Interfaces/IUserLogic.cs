using EnergyPlatform.Repository.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnergyPlatformProgram.BusinessLogic.Interfaces
{
    public interface IUserLogic
    {
        Task RemoveWithDevices(UserEntity user);
    }
}
