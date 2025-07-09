using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace APITrip.Kafka
{
    public class KafkaProducerService
    {
        private readonly string _bootstrapServers;
        private readonly string _topic;

        public KafkaProducerService(IConfiguration configuration)
        {
            _bootstrapServers = configuration["Kafka:BootstrapServers"];
            _topic = configuration["Kafka:Topic"];
        }

        public async Task ProduceAsync(string message)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        }
    }
}
