using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace KafkaLog
{
    public class KafkaManager
    {
        private static string Servers { get; set; } = "localhost:9092";
        
        public static IProducer<TKey, TValue> CreateProducer<TKey, TValue>()
        {
            var config = new ProducerConfig { BootstrapServers = Servers };
            return new ProducerBuilder<TKey,TValue>(config).Build();
        }

        public static IConsumer<TKey, TValue> CreateConsumer<TKey, TValue>()
        {
            var config = new ConsumerConfig
            {
                GroupId = Guid.NewGuid().ToString(),
                BootstrapServers = Servers,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            return new ConsumerBuilder<TKey,TValue>(config).Build();
        }
    }
}
