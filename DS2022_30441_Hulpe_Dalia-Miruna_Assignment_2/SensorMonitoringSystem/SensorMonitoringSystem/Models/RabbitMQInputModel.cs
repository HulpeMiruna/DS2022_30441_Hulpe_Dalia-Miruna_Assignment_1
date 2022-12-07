using System;

namespace SensorMonitoringSystem.Models
{
    public class RabbitMQInputModel
    {
        public Guid Device_id { get; set; } 

        public string Timestamp { get; set; }

        public double Measurement_value { get; set; }
    }
}
