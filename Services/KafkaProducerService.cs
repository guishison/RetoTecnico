using System;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using RetoTecnico.Models;

namespace RetoTecnico.Services
{
    public class KafkaProducerService : IDisposable, IKafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;

        public KafkaProducerService(IConfiguration configuration)
        {
            var config = new ProducerConfig { BootstrapServers = configuration["Kafka:BootstrapServers"] };
            _producer = new ProducerBuilder<Null, string>(config).Build();
            _topic = configuration["Kafka:TransactionStatusTopic"];
        }

        public async Task ProduceTransactionStatus(TransactionStatusUpdate statusUpdate)
        {
            try
            {
                var message = new Message<Null, string>
                {
                    Value = JsonSerializer.Serialize(statusUpdate)
                };
                var deliveryResult = await _producer.ProduceAsync(_topic, message);
                Console.WriteLine($"Mensaje enviado a Kafka | Topic: {_topic} | Partition: {deliveryResult.Partition} | Offset: {deliveryResult.Offset}");
            }
            catch (ProduceException<Null, string> e)
            {
                Console.WriteLine($"Error al enviar mensaje a Kafka: {e.Error.Reason}");
            }
        }

        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}