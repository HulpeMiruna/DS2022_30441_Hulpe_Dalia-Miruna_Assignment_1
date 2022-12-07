using RabbitMQ.Client;
using System;
using System.Collections.Generic;

namespace SensorMonitoringSystem.Interfaces
{
    public interface IRabitMQProducer
    {
        IConnection CreateConnection(); 
        public void SendMessage<T>(T messages, IConnection connection);
    }
}
