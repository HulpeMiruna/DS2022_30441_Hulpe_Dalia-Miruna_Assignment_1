using Newtonsoft.Json;
using RabbitMQ.Client;
using SensorMonitoringSystem.Interfaces;
using SensorMonitoringSystem.Models;
using System;
using System.Text;

namespace SensorMonitoringSystem.Implementation
{
    public class RabitMQProducer : IRabitMQProducer
    {
        private readonly string _userName = "tosugcxo";
        private readonly string _password = "5A_87u1KJrtf5LEVDIkoA8oGpYY-rMZ0";
        private readonly string _vhost = "tosugcxo";
        private readonly Uri _uri = new Uri("amqps://tosugcxo:5A_87u1KJrtf5LEVDIkoA8oGpYY-rMZ0@goose.rmq2.cloudamqp.com/tosugcxo");

        public IConnection CreateConnection()
        {
            //Here we specify the Rabbit MQ Server. we use rabbitmq docker image and use it
            var factory = new ConnectionFactory { Uri = _uri, VirtualHost = _vhost, UserName = _userName, Password = _password };

            var connection = factory.CreateConnection();

            return connection;
        }

        public void SendMessage<T>(T message,  IConnection connection)
        {
            using (var channel = connection.CreateModel())
            {
                //declare the queue after mentioning name and a few property related to that
                channel.QueueDeclare("SensorData", exclusive: false, durable: true, autoDelete: false, arguments: null);

                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                //put the data on to the product queue
                channel.BasicPublish(exchange: "", routingKey: "SensorData", null, body: body);
            };
        }
    }
}
