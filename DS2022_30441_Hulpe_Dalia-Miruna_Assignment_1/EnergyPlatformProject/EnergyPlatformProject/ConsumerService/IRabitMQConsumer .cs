
using System.Collections.Generic;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace EnergyPlatformProject.ConsumerService
{
    public interface IRabitMQConsumer
    {
        Task RecieveMessage(IConnection connection);

        IConnection CreateConnection();
    }
}
