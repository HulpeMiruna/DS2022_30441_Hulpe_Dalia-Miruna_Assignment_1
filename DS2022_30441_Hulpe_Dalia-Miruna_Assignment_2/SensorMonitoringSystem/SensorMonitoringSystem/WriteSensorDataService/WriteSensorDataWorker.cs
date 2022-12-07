using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualBasic.FileIO;
using RabbitMQ.Client;
using SensorMonitoringSystem.Interfaces;
using SensorMonitoringSystem.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SensorMonitoringSystem.WriteSensorDataService
{
    public class WriteSensorDataWorker : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer = null;
        private TextFieldParser CsvParser;

        public WriteSensorDataWorker(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public TextFieldParser InitializeDataParser()
        {
            var path = $"{System.IO.Directory.GetCurrentDirectory()}{@"\wwwroot\sensor.csv"}";

            TextFieldParser csvParser = new TextFieldParser(path);

            csvParser.SetDelimiters(new string[] { "," });

            return csvParser;
        }

        public RabbitMQInputModel ReadCsvFile(TextFieldParser csvParser)
        {
            if (csvParser.EndOfData)
            {
                return null;
            }

            string[] fields = csvParser.ReadFields();
            var value = new RabbitMQInputModel()
            {
                Timestamp = DateTime.Now.ToString(),
                Device_id = new Guid("a10aa392-a9ad-43ac-441d-08dad7beb406"),
                Measurement_value = double.Parse(fields[0])
            };

            return value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                CsvParser = InitializeDataParser();
                var producer = scope.ServiceProvider.GetRequiredService<IRabitMQProducer>();
                var connection = producer.CreateConnection();
                _timer = new Timer(async t => await SendMessage(t, connection), null, TimeSpan.Zero, TimeSpan.FromSeconds(10));
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public Task SendMessage(object state, IConnection connection)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var producer = scope.ServiceProvider.GetRequiredService<IRabitMQProducer>();
                var data = ReadCsvFile(CsvParser);
                if (data != null)
                {
                    producer.SendMessage(data, connection);
                }
            }

            return Task.CompletedTask;
        }
    }
}
