using EnergyPlatform.Repository.Entitys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnergyPlatformProgram.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity> FindByEmailAsync(string email);
    }
}
