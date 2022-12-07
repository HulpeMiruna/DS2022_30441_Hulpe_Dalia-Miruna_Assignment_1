using AutoMapper;
using EnergyPlatform.Repository.Entitys;
using EnergyPlatformProgram.BusinessLogic.Constants;
using EnergyPlatformProgram.BusinessLogic.Interfaces;
using EnergyPlatformProject.ConsumerService;
using EnergyPlatformProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EnergyPlatformProject.ReadSensorDataService
{
    public class ReadSensorDataWorker : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer = null;

        public ReadSensorDataWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var consumer = scope.ServiceProvider.GetRequiredService<IRabitMQConsumer>();
                var connection = consumer.CreateConnection();
                _timer = new Timer(async t => await ReadMessage(t, connection), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public async Task ReadMessage(object state, IConnection connection)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var consumer = scope.ServiceProvider.GetRequiredService<IRabitMQConsumer>();
                await consumer.RecieveMessage(connection);
            }
        }
    }
}
