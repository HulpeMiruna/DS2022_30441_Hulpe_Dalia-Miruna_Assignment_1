using System.Threading.Tasks;
using AutoMapper;
using EnergyPlatformProgram.BusinessLogic.Models;
using EnergyPlatformProgram.Mappers;
using EnergyPlatformProgram.Repository.Implementations;
using EnergyPlatformProgram.Repository.Models;
using EnergyPlatformProject.Models;
using Microsoft.AspNetCore.SignalR;

namespace EnergyPlatformProgram.Hubs
{
    public interface INotificationService
    {
        Task SendMessage(string message);
    }
}
