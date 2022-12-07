using EnergyPlatformProgram.BusinessLogic.Interfaces;
using EnergyPlatformProgram.BusinessLogic.Models;
using EnergyPlatformProgram.Hubs;
using EnergyPlatformProgram.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnergyPlatformProject.ConsumerService
{
    public class RabitMQConsumer : IRabitMQConsumer
    {
        private readonly string _userName = "tosugcxo";
        private readonly string _password = "5A_87u1KJrtf5LEVDIkoA8oGpYY-rMZ0";
        private readonly string _vhost = "tosugcxo";
        private readonly Uri _uri = new Uri("amqps://tosugcxo:5A_87u1KJrtf5LEVDIkoA8oGpYY-rMZ0@goose.rmq2.cloudamqp.com/tosugcxo");
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly INotificationService _notificationHub;

        public RabitMQConsumer(IServiceScopeFactory scopeFactory, INotificationService notificationHub)
        {
            _scopeFactory = scopeFactory;
            _notificationHub = notificationHub;
        }

        public IConnection CreateConnection()
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory { Uri = _uri, VirtualHost = _vhost, UserName = _userName, Password = _password };

            var connection = factory.CreateConnection();

            return connection;
        }

        public async Task RecieveMessage(IConnection connection)
        {
            using (var channel = connection.CreateModel())
            {
                //declare the queue after mentioning name and a few property related to that
                channel.QueueDeclare("SensorData", exclusive: false, durable: true, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, eventArgs) => {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var value = JsonConvert.DeserializeObject<RabbitMQOutputModel>(message);
                    var consumption = new ConsumtionModel()
                    {
                        Id = Guid.NewGuid(),
                        DeviceId = value.Device_id,
                        Date = DateTime.Parse(value.Timestamp),
                        Consumtion = (float)value.Measurement_value
                    };

                    //Console.WriteLine($"SensorData message received: {message}");

                    using (var scope = _scopeFactory.CreateScope())
                    {
                        var deviceLogic = scope.ServiceProvider.GetRequiredService<IDeviceLogic>();
                        await deviceLogic.AddConsumtionAsync(consumption);

                        var device = await deviceLogic.FindByIdAsync(value.Device_id);

                        if (value.Measurement_value > double.Parse(device.MaximuHourlyEnergyConsumtion))
                        {
                            await _notificationHub.SendMessage($"This value exceded the limit{value.Measurement_value} for the device at address: {device.Address}");
                        }
                    };
                };
                //read the message
                channel.BasicConsume(queue: "SensorData", autoAck: true, consumer: consumer);
                //Console.ReadKey();
            };
        }
    }
}
