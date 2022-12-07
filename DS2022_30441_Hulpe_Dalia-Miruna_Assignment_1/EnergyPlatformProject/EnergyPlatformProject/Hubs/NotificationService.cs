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
    public class NotificationService  : Hub, INotificationService
    {
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationService(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

    }
}
