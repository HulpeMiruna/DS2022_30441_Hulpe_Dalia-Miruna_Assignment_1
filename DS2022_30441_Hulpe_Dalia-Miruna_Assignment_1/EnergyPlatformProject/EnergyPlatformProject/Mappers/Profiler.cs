using AutoMapper;
using EnergyPlatformProgram.BusinessLogic.Models;
using EnergyPlatformProgram.Repository.Implementations;
using EnergyPlatformProgram.Repository.Models;
using EnergyPlatformProject.Models;

namespace EnergyPlatformProgram.Mappers
{
    public class Profiler: Profile
    {
        public Profiler()
        {
            #region BusinessToView
            CreateMap<DeviceModel, DeviceViewModel>();
            #endregion

            #region ViewToBusiness
            CreateMap<DeviceViewModel, DeviceModel>();
            #endregion

            #region BusinessToRepo
            CreateMap<DeviceModel, DeviceEntity>();
            CreateMap<ConsumtionModel, ConsumptionEntity>();
            #endregion

            #region RepoToBusiness
            CreateMap<DeviceEntity, DeviceModel>();
            CreateMap<ConsumptionEntity, ConsumtionModel>();
            #endregion
        }
    }
}
