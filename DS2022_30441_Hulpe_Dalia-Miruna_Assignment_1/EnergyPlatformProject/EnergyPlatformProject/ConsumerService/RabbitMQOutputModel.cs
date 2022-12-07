using System;

namespace EnergyPlatformProject.ConsumerService
{
    public class RabbitMQOutputModel
    {
        public Guid Device_id { get; set; } 

        public string Timestamp { get; set; }

        public double Measurement_value { get; set; }
    }
}
